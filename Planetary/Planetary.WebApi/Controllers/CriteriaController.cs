using MediatR;
using Microsoft.AspNetCore.Mvc;
using Planetary.Application.Commands;
using Planetary.Application.Queries;
using Planetary.Application.Queries.GetCriteriaById;
using Planetary.Domain.Models;

namespace Planetary.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CriteriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criteria>>> GetAllCriteria()
        {
            var query = new GetAllCriteriaQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Criteria>> GetCriteriaById([FromRoute] Guid id)
        {
            var query = new GetCriteriaByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Criteria>> AddCriteria([FromBody] AddCriteriaCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCriteriaById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCriteria([FromRoute] Guid id, [FromBody] UpdateCriteriaCommand command)
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCriteria([FromRoute] Guid id)
        {
            var command = new DeleteCriteriaByIdCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}