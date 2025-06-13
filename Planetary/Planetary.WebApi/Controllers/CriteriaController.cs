using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Criteria>> GetCriteriaById(Guid id)
        {
            var query = new GetCriteriaByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}