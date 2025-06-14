using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planetary.Application.Commands;
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Planet>> GetPlanetById([FromRoute] Guid id)
        {
            var query = new GetPlanetByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlanet([FromRoute] Guid id, [FromBody] UpdatePlanetCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL must match the ID in the request body");
            }
            
            var result = await _mediator.Send(command);
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}