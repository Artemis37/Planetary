using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planetary.Application.Commands;
using Planetary.Application.Queries;

namespace Planetary.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Viewer")]
    public class PlanetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPlanetQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPlanetByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        //[HttpPost]
        //[Authorize(Policy = "PlanetAdmin")]
        //public async Task<IActionResult> Create(AddPlanetCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetById), new { id = result }, result);
        //}

        [HttpPut("{id}")]
        [Authorize(Policy = "EditPlanet")]
        public async Task<IActionResult> Update(Guid id, UpdatePlanetCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : NotFound();
        }

        //[HttpDelete("{id}")]
        //[Authorize(Policy = "PlanetAdmin")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _mediator.Send(new DeletePlanetCommand { Id = id });
        //    return result ? NoContent() : NotFound();
        //}
    }
}