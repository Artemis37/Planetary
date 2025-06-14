using System;
using System.Collections.Generic;

namespace Planetary.Domain.Models
{
    public class Criteria
    {
        public Criteria()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }

        public Criteria(
            string name, 
            string description, 
            string category, 
            double minimumThreshold,
            double maximumThreshold,
            string unit,
            double weight,
            bool isRequired)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Category = category;
            MinimumThreshold = minimumThreshold;
            MaximumThreshold = maximumThreshold;
            Unit = unit;
            Weight = weight;
            IsRequired = isRequired;
            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public double MinimumThreshold { get; private set; }
        public double MaximumThreshold { get; private set; }
        public string Unit { get; private set; } = string.Empty;
        public double Weight { get; private set; }
        public bool IsRequired { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }

        private readonly List<PlanetCriteria> _planetCriteria = new();
        public IReadOnlyCollection<PlanetCriteria> PlanetCriteria => _planetCriteria.AsReadOnly();

        public void AddPlanetCriteria(PlanetCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            _planetCriteria.Add(criteria);
        }

        public void RemovePlanetCriteria(PlanetCriteria criteria)
        {
            _planetCriteria.Remove(criteria);
        }

        public void UpdateDetails(
            string name,
            string description,
            string category)
        {
            Name = name;
            Description = description;
            Category = category;
            ModifiedDate = DateTime.UtcNow;
        }

        public void UpdateThresholds(
            double minimumThreshold,
            double maximumThreshold,
            string unit)
        {
            MinimumThreshold = minimumThreshold;
            MaximumThreshold = maximumThreshold;
            Unit = unit;
            ModifiedDate = DateTime.UtcNow;
        }
        
        public void UpdateWeightAndRequirement(double weight, bool isRequired)
        {
            Weight = weight;
            IsRequired = isRequired;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}