using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planetary.Application.Commands;
using Planetary.Application.Queries;
using Planetary.Application.Queries.GetCriteriaById;
using Planetary.Domain.Models;

namespace Planetary.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Viewer")]
    public class CriteriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CriteriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCriteriaQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetCriteriaByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "EditCriteria")]
        public async Task<IActionResult> Create(AddCriteriaCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "EditCriteria")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCriteriaCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "EditCriteria")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteCriteriaByIdCommand { Id = id });
            return result ? NoContent() : NotFound();
        }
    }
}