using System;
using System.Collections.Generic;

namespace Planetary.Domain.Models
{
    public class Planet
    {
        private readonly List<PlanetCriteria> _planetCriteria = new();

        public Planet()
        {
            Id = Guid.NewGuid();
        }

        public Planet(
            string name, 
            string stellarSystem, 
            double distanceFromEarth,
            double radius,
            double mass)
        {
            Id = Guid.NewGuid();
            Name = name;
            StellarSystem = stellarSystem;
            DistanceFromEarth = distanceFromEarth;
            Radius = radius;
            Mass = mass;
            DiscoveryDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string StellarSystem { get; private set; } = string.Empty;
        public double DistanceFromEarth { get; private set; } // in light years
        public double Radius { get; private set; } // in km
        public double Mass { get; private set; } // in Earth masses
        public double SurfaceTemperature { get; private set; } // in Kelvin
        public double SurfaceGravity { get; private set; } // in g (Earth gravity = 1)
        public bool HasAtmosphere { get; private set; }
        public string AtmosphericComposition { get; private set; } = string.Empty;
        public double AtmosphericPressure { get; private set; } // in Earth atmospheres
        public bool HasWater { get; private set; }
        public double WaterCoverage { get; private set; } // percentage
        public string PlanetType { get; private set; } = string.Empty; // e.g. Terrestrial, Gas Giant, etc.
        public DateTime DiscoveryDate { get; private set; }
        
        // Read-only public property that exposes the private list
        public IReadOnlyCollection<PlanetCriteria> PlanetCriteria => _planetCriteria.AsReadOnly();

        // Methods to manipulate the private collection
        public void AddCriteria(PlanetCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
                
            _planetCriteria.Add(criteria);
        }

        public void RemoveCriteria(PlanetCriteria criteria)
        {
            _planetCriteria.Remove(criteria);
        }

        public void UpdateBasicInfo(string name, string stellarSystem)
        {
            Name = name;
            StellarSystem = stellarSystem;
        }
        
        public void UpdateOrbitalData(double distanceFromEarth)
        {
            DistanceFromEarth = distanceFromEarth;
        }
        
        public void UpdatePhysicalProperties(double radius, double mass)
        {
            Radius = radius;
            Mass = mass;
        }

        public void UpdateSurfaceData(
            double surfaceTemperature,
            double surfaceGravity,
            bool hasAtmosphere,
            string atmosphericComposition,
            double atmosphericPressure)
        {
            SurfaceTemperature = surfaceTemperature;
            SurfaceGravity = surfaceGravity;
            HasAtmosphere = hasAtmosphere;
            AtmosphericComposition = atmosphericComposition;
            AtmosphericPressure = atmosphericPressure;
        }

        public void UpdateWaterData(bool hasWater, double waterCoverage)
        {
            HasWater = hasWater;
            WaterCoverage = waterCoverage;
        }
        
        public void SetPlanetType(string planetType)
        {
            PlanetType = planetType;
        }
        
        public void UpdateDiscoveryDate(DateTime discoveryDate)
        {
            DiscoveryDate = discoveryDate;
        }
    }
}