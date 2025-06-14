using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Commands
{
    public class UpdateCriteriaCommand : IRequest<Criteria?>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double MinimumThreshold { get; set; }
        public double MaximumThreshold { get; set; }
        public string Unit { get; set; } = string.Empty;
        public double Weight { get; set; }
        public bool IsRequired { get; set; }
    }

    public class UpdateCriteriaCommandHandler : IRequestHandler<UpdateCriteriaCommand, Criteria?>
    {
        private readonly ICriteriaRepository _repository;

        public UpdateCriteriaCommandHandler(ICriteriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Criteria?> Handle(UpdateCriteriaCommand request, CancellationToken cancellationToken)
        {
            var criteria = await _repository.GetByIdAsync(request.Id);
            if (criteria == null)
            {
                return null;
            }

            criteria.UpdateDetails(
                request.Name,
                request.Description,
                request.Category
            );

            criteria.UpdateThresholds(
                request.MinimumThreshold,
                request.MaximumThreshold,
                request.Unit
            );

            criteria.UpdateWeightAndRequirement(
                request.Weight,
                request.IsRequired
            );

            await _repository.UpdateAsync(criteria);
            return criteria;
        }
    }
}