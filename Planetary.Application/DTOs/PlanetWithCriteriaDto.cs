using Planetary.Domain.Models;

namespace Planetary.Application.DTOs
{
    public class PlanetWithCriteriaDto
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
        public List<PlanetCriteriaDto> Criteria { get; set; } = new List<PlanetCriteriaDto>();

        // Create from Planet entity
        public static PlanetWithCriteriaDto FromPlanet(Planet planet)
        {
            var dto = new PlanetWithCriteriaDto
            {
                Id = planet.Id,
                Name = planet.Name,
                StellarSystem = planet.StellarSystem,
                DistanceFromEarth = planet.DistanceFromEarth,
                Radius = planet.Radius,
                Mass = planet.Mass,
                SurfaceTemperature = planet.SurfaceTemperature,
                SurfaceGravity = planet.SurfaceGravity,
                HasAtmosphere = planet.HasAtmosphere,
                AtmosphericComposition = planet.AtmosphericComposition,
                AtmosphericPressure = planet.AtmosphericPressure,
                HasWater = planet.HasWater,
                WaterCoverage = planet.WaterCoverage,
                PlanetType = planet.PlanetType,
                DiscoveryDate = planet.DiscoveryDate,
                UserId = planet.UserId
            };

            // Add criteria information
            if (planet.PlanetCriteria != null)
            {
                foreach (var pc in planet.PlanetCriteria)
                {
                    dto.Criteria.Add(new PlanetCriteriaDto
                    {
                        Id = pc.Id,
                        PlanetId = pc.PlanetId,
                        CriteriaId = pc.CriteriaId,
                        Value = pc.Value,
                        Score = pc.Score,
                        IsMet = pc.IsMet,
                        Notes = pc.Notes,
                        EvaluationDate = pc.EvaluationDate,
                        CriteriaName = pc.Criteria?.Name ?? string.Empty,
                        CriteriaDescription = pc.Criteria?.Description ?? string.Empty,
                        CriteriaCategory = pc.Criteria?.Category ?? string.Empty,
                        MinimumThreshold = pc.Criteria?.MinimumThreshold ?? 0,
                        MaximumThreshold = pc.Criteria?.MaximumThreshold ?? 0,
                        Unit = pc.Criteria?.Unit ?? string.Empty,
                        Weight = pc.Criteria?.Weight ?? 0,
                        IsRequired = pc.Criteria?.IsRequired ?? false
                    });
                }
            }

            return dto;
        }
    }

    public class PlanetCriteriaDto
    {
        public Guid Id { get; set; }
        public Guid PlanetId { get; set; }
        public Guid CriteriaId { get; set; }
        public double Value { get; set; }
        public double Score { get; set; }
        public bool IsMet { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime EvaluationDate { get; set; }
        
        // Criteria information
        public string CriteriaName { get; set; } = string.Empty;
        public string CriteriaDescription { get; set; } = string.Empty;
        public string CriteriaCategory { get; set; } = string.Empty;
        public double MinimumThreshold { get; set; }
        public double MaximumThreshold { get; set; }
        public string Unit { get; set; } = string.Empty;
        public double Weight { get; set; }
        public bool IsRequired { get; set; }
    }
}