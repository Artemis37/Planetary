using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Commands
{
    public class AddPlanetCommand : IRequest<Planet>
    {
        public string Name { get; set; } = string.Empty;
        public string StellarSystem { get; set; } = string.Empty;
        public double DistanceFromEarth { get; set; }
        public double Radius { get; set; }
        public double Mass { get; set; }
        public double SurfaceTemperature { get; set; } = 0;
        public double SurfaceGravity { get; set; } = 0;
        public bool HasAtmosphere { get; set; } = false;
        public string AtmosphericComposition { get; set; } = string.Empty;
        public double AtmosphericPressure { get; set; } = 0;
        public bool HasWater { get; set; } = false;
        public double WaterCoverage { get; set; } = 0;
        public string PlanetType { get; set; } = string.Empty;
        public DateTime? DiscoveryDate { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddPlanetCommandHandler : IRequestHandler<AddPlanetCommand, Planet>
    {
        private readonly IPlanetRepository _repository;
        private readonly IUserRepository _userRepository;

        public AddPlanetCommandHandler(IPlanetRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Planet> Handle(AddPlanetCommand request, CancellationToken cancellationToken)
        {
            // Ensure the user exists
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {request.UserId} not found");
            }

            // Check if user has permission to create planets
            if (user.UserType != UserType.PlanetAdmin && user.UserType != UserType.SuperAdmin)
            {
                throw new UnauthorizedAccessException("User does not have permission to create planets");
            }

            // Create new planet with owner
            var planet = new Planet(
                request.Name,
                request.StellarSystem,
                request.DistanceFromEarth,
                request.Radius,
                request.Mass,
                request.UserId
            );

            // Set additional planet properties
            planet.UpdateSurfaceData(
                request.SurfaceTemperature,
                request.SurfaceGravity,
                request.HasAtmosphere,
                request.AtmosphericComposition,
                request.AtmosphericPressure
            );

            planet.UpdateWaterData(
                request.HasWater,
                request.WaterCoverage
            );

            planet.SetPlanetType(request.PlanetType);

            if (request.DiscoveryDate.HasValue)
            {
                planet.UpdateDiscoveryDate(request.DiscoveryDate.Value);
            }

            // Add planet to database
            await _repository.AddAsync(planet);

            return planet;
        }
    }
}