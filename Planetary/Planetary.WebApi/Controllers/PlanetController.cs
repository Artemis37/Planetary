using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planetary.Application.Queries;
using Planetary.Domain.Models;

namespace Planetary.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planet>>> GetAllPlanets()
        {
            var query = new GetAllPlanetQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}