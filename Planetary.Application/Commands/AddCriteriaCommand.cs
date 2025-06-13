using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Commands
{
    public class AddCriteriaCommand : IRequest<Criteria>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double MinimumThreshold { get; set; }
        public double MaximumThreshold { get; set; }
        public string Unit { get; set; } = string.Empty;
        public double Weight { get; set; }
        public bool IsRequired { get; set; }
    }

    public class AddCriteriaCommandHandler : IRequestHandler<AddCriteriaCommand, Domain.Models.Criteria>
    {
        private readonly ICriteriaRepository _repository;

        public AddCriteriaCommandHandler(ICriteriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Criteria> Handle(AddCriteriaCommand request, CancellationToken cancellationToken)
        {
            var criteria = new Domain.Models.Criteria(
                request.Name,
                request.Description,
                request.Category,
                request.MinimumThreshold,
                request.MaximumThreshold,
                request.Unit,
                request.Weight,
                request.IsRequired
            );

            await _repository.AddAsync(criteria);

            return criteria;
        }
    }
}