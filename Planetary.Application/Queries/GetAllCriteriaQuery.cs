using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Queries
{
    public class GetAllCriteriaQuery : IRequest<IEnumerable<Criteria>>
    {
    }

    public class GetAllCriteriaQueryHandler : IRequestHandler<GetAllCriteriaQuery, IEnumerable<Criteria>>
    {
        private readonly ICriteriaRepository _repository;

        public GetAllCriteriaQueryHandler(ICriteriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Criteria>> Handle(GetAllCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}