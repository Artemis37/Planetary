using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Queries
{
    public class GetAllPlanetQuery : IRequest<IEnumerable<Planet>>
    {
    }

    public class GetAllPlanetQueryHandler : IRequestHandler<GetAllPlanetQuery, IEnumerable<Planet>>
    {
        private readonly IPlanetRepository _repository;

        public GetAllPlanetQueryHandler(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Planet>> Handle(GetAllPlanetQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}