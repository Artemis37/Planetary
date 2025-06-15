using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planetary.Application.Commands
{
    public class PlanetCriteriaUpdateDto
    {
        public Guid CriteriaId { get; set; }
        public double Value { get; set; }
        public double Score { get; set; }
        public bool IsMet { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    public class UpdatePlanetCommand : IRequest<Planet?>
    {
        public Guid PlanetId { get; set; }  // Changed from Id to PlanetId for better clarity
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
        public List<PlanetCriteriaUpdateDto> Criteria { get; set; } = new List<PlanetCriteriaUpdateDto>();
    }

    public class UpdatePlanetCommandHandler : IRequestHandler<UpdatePlanetCommand, Planet?>
    {
        private readonly IPlanetRepository _repository;
        private readonly ICriteriaRepository _criteriaRepository;

        public UpdatePlanetCommandHandler(IPlanetRepository repository, ICriteriaRepository criteriaRepository)
        {
            _repository = repository;
            _criteriaRepository = criteriaRepository;
        }

        public async Task<Planet?> Handle(UpdatePlanetCommand request, CancellationToken cancellationToken)
        {
            var planet = await _repository.GetByIdAsync(request.PlanetId);
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

            //planet.EmptyCriteria();

            //if (request.Criteria != null && request.Criteria.Count > 0)
            //{
            //    foreach (var criteriaDto in request.Criteria)
            //    {
            //        var criteriaDefinition = await _criteriaRepository.GetByIdAsync(criteriaDto.CriteriaId);
            //        if (criteriaDefinition != null)
            //        {
            //            var planetCriteria = new PlanetCriteria(
            //                planet.Id,
            //                criteriaDefinition.Id,
            //                criteriaDto.Value,
            //                criteriaDto.Score,
            //                criteriaDto.IsMet,
            //                criteriaDto.Notes
            //            );

            //            planet.AddCriteria(planetCriteria);
            //        }
            //    }
            //}

            await _repository.UpdateAsync(planet);
            return planet;
        }
    }
}