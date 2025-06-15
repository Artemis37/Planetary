using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Commands
{
    public class UpdatePlanetCommand : IRequest<Planet?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StellarSystem { get; set; } = string.Empty;
        public double DistanceFromEarth { get; set; }
        public double Radius { get; set; }
        public double Mass { get; set; }
        public double SurfaceTemperature { get; set; }
        public double SurfaceGravity { get; set; }
        public bool HasAtmosphere { get; set; }
        public string AtmosphericComposition { get; set; } = string.Empty;
        public double AtmosphericPressure { get; set; }
        public bool HasWater { get; set; }
        public double WaterCoverage { get; set; }
        public string PlanetType { get; set; } = string.Empty;
        public DateTime DiscoveryDate { get; set; }
        public Guid UserId { get; set; }
    }

    public class UpdatePlanetCommandHandler : IRequestHandler<UpdatePlanetCommand, Planet?>
    {
        private readonly IPlanetRepository _repository;

        public UpdatePlanetCommandHandler(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Planet?> Handle(UpdatePlanetCommand request, CancellationToken cancellationToken)
        {
            var planet = await _repository.GetByIdAsync(request.Id);
            if (planet == null)
            {
                return null;
            }

            planet.UpdateBasicInfo(
                request.Name,
                request.StellarSystem
            );

            planet.UpdateOrbitalData(
                request.DistanceFromEarth
            );

            planet.UpdatePhysicalProperties(
                request.Radius,
                request.Mass
            );

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
            planet.UpdateDiscoveryDate(request.DiscoveryDate);
            planet.UpdateUserId(request.UserId);

            await _repository.UpdateAsync(planet);
            return planet;
        }
    }
}