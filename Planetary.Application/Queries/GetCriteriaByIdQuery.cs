using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Queries.GetCriteriaById
{
    public class GetCriteriaByIdQuery : IRequest<Criteria?>
    {
        public Guid Id { get; set; }
    }

    public class GetCriteriaByIdQueryHandler : IRequestHandler<GetCriteriaByIdQuery, Criteria?>
    {
        private readonly ICriteriaRepository _repository;

        public GetCriteriaByIdQueryHandler(ICriteriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Criteria?> Handle(GetCriteriaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}