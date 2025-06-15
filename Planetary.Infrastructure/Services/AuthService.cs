using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Planetary.Application.DTOs;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Planetary.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IPlanetRepository planetRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _planetRepository = planetRepository;
            _configuration = configuration;
        }

        public async Task<(bool Success, User? User)> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return (false, null);
            }

            // Update last login time
            user.SetLastLogin();
            await _userRepository.UpdateAsync(user);

            return (true, user);
        }

        public async Task<RegisterResponse> RegisterUserAsync(
            string username, 
            string email, 
            string password, 
            string firstName, 
            string lastName,
            string organization,
            string jobTitle,
            string researchInterests,
            string preferredPlanetarySystem,
            UserType userType)
        {
            // Check if username already exists
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(username);
            if (existingUserByUsername != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Username already exists"
                };
            }

            // Check if email already exists
            var existingUserByEmail = await _userRepository.GetByEmailAsync(email);
            if (existingUserByEmail != null)
            {
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            // Hash the password
            var passwordHash = HashPassword(password);

            // Create the new user
            var user = new User(
                username,
                email,
                passwordHash,
                firstName,
                lastName,
                userType);

            // Update additional profile information
            user.UpdateProfile(
                firstName,
                lastName,
                organization,
                jobTitle,
                researchInterests,
                preferredPlanetarySystem);

            // Add the user to the database
            var addedUser = await _userRepository.AddAsync(user);

            // Create response
            return new RegisterResponse
            {
                Id = addedUser.Id,
                Username = addedUser.Username,
                Email = addedUser.Email,
                FirstName = addedUser.FirstName,
                LastName = addedUser.LastName,
                UserType = addedUser.UserType.ToString(),
                CreatedDate = addedUser.CreatedDate,
                Success = true,
                Message = "User registered successfully"
            };
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? "YourSuperSecretKeyGoesHere123!@#");
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("userType", user.UserType.ToString())
            };

            // Add owned planet claims for PlanetAdmin users
            if (user.UserType == UserType.PlanetAdmin)
            {
                // Get planets owned by this user
                var ownedPlanets = await _planetRepository.GetPlanetsByUserIdAsync(user.Id);
                foreach (var planet in ownedPlanets)
                {
                    claims.Add(new Claim("OwnedPlanet", planet.Id.ToString()));
                }
            }

            var tokenExpiration = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpirationHours"] ?? "24"));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponse> GetLoginResponseAsync(User user)
        {
            var token = await GenerateJwtToken(user);
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenExpiration = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpirationHours"] ?? "24"));

            return new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType.ToString(),
                ExpiresAt = tokenExpiration
            };
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // In a real application, this should use a secure password hashing algorithm like bcrypt
            // This is a simple implementation for demonstration purposes
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}