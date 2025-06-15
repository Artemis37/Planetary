using MediatR;
using Planetary.Application.DTOs;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;

namespace Planetary.Application.Queries
{
    public class GetPlanetByIdQuery : IRequest<PlanetWithCriteriaDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetPlanetByIdQueryHandler : IRequestHandler<GetPlanetByIdQuery, PlanetWithCriteriaDto?>
    {
        private readonly IPlanetRepository _repository;

        public GetPlanetByIdQueryHandler(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlanetWithCriteriaDto?> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken)
        {
            var planet = await _repository.GetByIdAsync(request.Id);
            if (planet == null)
            {
                return null;
            }

            return PlanetWithCriteriaDto.FromPlanet(planet);
        }
    }
}