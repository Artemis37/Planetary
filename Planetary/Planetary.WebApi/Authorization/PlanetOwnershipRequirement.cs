using Microsoft.AspNetCore.Authorization;
using Planetary.Application.Interfaces;
using System.Security.Claims;

namespace Planetary.WebApi.Authorization
{
    /// <summary>
    /// Authorization requirement that checks if a user is the owner of a planet
    /// </summary>
    public class PlanetOwnershipRequirement : IAuthorizationRequirement
    {
    }

    public class PlanetOwnershipHandler : AuthorizationHandler<PlanetOwnershipRequirement>
    {
        private readonly IPlanetRepository _planetRepository;

        public PlanetOwnershipHandler(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PlanetOwnershipRequirement requirement)
        {
            // Get the user ID from the claims
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return; // No valid user ID found in claims
            }

            var planetId = Guid.Empty;
            if (context.Resource is HttpContext httpContext)
            {
                planetId = Guid.Parse(((string?)httpContext.GetRouteValue("id")) ?? string.Empty);
            }

            // Get the planet from repository
            var planet = await _planetRepository.GetByIdAsync(planetId);
            if (planet == null)
            {
                return; // Planet not found
            }

            // Check if the user is the owner of the planet
            if (planet.UserId == userGuid)
            {
                context.Succeed(requirement);
            }
        }
    }
}