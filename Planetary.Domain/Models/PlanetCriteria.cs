using System;

namespace Planetary.Domain.Models
{
    public class PlanetCriteria
    {
        public PlanetCriteria()
        {
            Id = Guid.NewGuid();
            EvaluationDate = DateTime.UtcNow;
        }

        public PlanetCriteria(
            Guid planetId, 
            Guid criteriaId, 
            double value, 
            double score, 
            bool isMet,
            string notes)
        {
            Id = Guid.NewGuid();
            PlanetId = planetId;
            CriteriaId = criteriaId;
            Value = value;
            Score = score;
            IsMet = isMet;
            Notes = notes;
            EvaluationDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid PlanetId { get; private set; }
        public Guid CriteriaId { get; private set; }
        public double Value { get; private set; } // the actual value measured for this planet and criterion
        public double Score { get; private set; } // calculated score based on how well the planet meets the criterion
        public bool IsMet { get; private set; } // whether the planet meets this criterion
        public string Notes { get; private set; } = string.Empty;
        public DateTime EvaluationDate { get; private set; }
        
        // Navigation properties
        public virtual Planet Planet { get; private set; } = null!;
        public virtual Criteria Criteria { get; private set; } = null!;
        
        public void UpdateValue(double value)
        {
            Value = value;
            EvaluationDate = DateTime.UtcNow;
        }
        
        public void UpdateScore(double score, bool isMet)
        {
            Score = score;
            IsMet = isMet;
            EvaluationDate = DateTime.UtcNow;
        }
        
        public void UpdateNotes(string notes)
        {
            Notes = notes;
            EvaluationDate = DateTime.UtcNow;
        }
        
        public void SetNavigationProperties(Planet planet, Criteria criteria)
        {
            Planet = planet ?? throw new ArgumentNullException(nameof(planet));
            Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }
    }
}