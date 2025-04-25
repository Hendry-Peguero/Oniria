using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Application.Features.Organization.Commands;
using Oniria.Core.Application.Features.Organization.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Organization.Request;
using WebApi.DreamHouse.Controllers.General;

namespace OniriaApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
    public class OrganizationController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrganizationEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllOrganizationAsyncQuery());

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null || !result.Data.Any())
                return NoContent();

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await Mediator.Send(new GetOrganizationByIdAsyncQuery { Id = id });

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrganizationEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
        {
            var result = await Mediator.Send(new CreateOrganizationAsyncCommand { Request = request });

            if (!result.IsSuccess)
                return BadRequest(result.LastMessage());

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateOrganizationRequest request)
        {
            if (id != request.Id)
                return BadRequest("The provided ID does not match the request.");

            var result = await Mediator.Send(new UpdateOrganizationAsyncCommand { Request = request });

            if (!result.IsSuccess)
            {
                if (result.Messages.Any(e => e.Contains("not found", StringComparison.OrdinalIgnoreCase)))
                    return NotFound(result.LastMessage());

                return BadRequest(result.LastMessage());
            }

            return Ok(result.Data);
        }
    }
}
