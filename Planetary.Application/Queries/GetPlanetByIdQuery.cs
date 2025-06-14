using MediatR;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Queries
{
    public class GetPlanetByIdQuery : IRequest<Planet?>
    {
        public Guid Id { get; set; }
    }

    public class GetPlanetByIdQueryHandler : IRequestHandler<GetPlanetByIdQuery, Planet?>
    {
        private readonly IPlanetRepository _repository;

        public GetPlanetByIdQueryHandler(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Planet?> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}