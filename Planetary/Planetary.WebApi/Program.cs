using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Planetary.Domain.Models;
using Planetary.Infrastructure.Context;
using Planetary.WebApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure JWT Authentication
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings?.Secret ?? "YourSuperSecretKeyGoesHere123!@#");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Configure Policy-Based Authorization
builder.Services.AddAuthorization(options =>
{
    // Basic Role Policies
    options.AddPolicy("SuperAdmin", policy => policy.RequireRole(UserType.SuperAdmin.ToString()));
    options.AddPolicy("PlanetAdmin", policy => policy.RequireRole(UserType.PlanetAdmin.ToString(), UserType.SuperAdmin.ToString()));
    options.AddPolicy("Viewer", policy => policy.RequireRole(UserType.Viewer.ToString(), UserType.PlanetAdmin.ToString(), UserType.SuperAdmin.ToString()));

    // Resource-Based Policies
    options.AddPolicy("EditPlanet", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole(UserType.SuperAdmin.ToString()) || 
            (context.User.IsInRole(UserType.PlanetAdmin.ToString()) && 
             context.Resource != null && 
             context.User.HasClaim(c => c.Type == "OwnedPlanet" && c.Value == ((Guid)context.Resource).ToString()))));

    options.AddPolicy("EditCriteria", policy =>
        policy.RequireRole(UserType.SuperAdmin.ToString()));
});

// Configure Swagger/OpenAPI with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Planetary API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add dependency injection for application services
builder.Services.AddDependencyInjection(builder.Configuration);

var app = builder.Build();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PlanetaryContext>();
        context.Database.Migrate(); // Apply any pending migrations
        // Optionally seed initial data here if needed
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add the authentication middleware before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// JWT Settings for Authentication
/// </summary>
public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; }
}
