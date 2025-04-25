using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Application.Features.Employee.Commands;
using Oniria.Core.Application.Features.Employee.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Employee.Request;
using WebApi.DreamHouse.Controllers.General;

namespace OniriaApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
    public class EmployeeController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllEmployeeAsyncQuery());

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null || !result.Data.Any())
                return NoContent();

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await Mediator.Send(new GetEmployeeByIdAsyncQuery { Id = id });

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
            var result = await Mediator.Send(new CreateEmployeeAsyncCommand { Request = request });

            if (!result.IsSuccess)
                return BadRequest(result.LastMessage());

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeRequest request)
        {
            var result = await Mediator.Send(new UpdateEmployeeAsyncCommand { Request = request });

            if (!result.IsSuccess)
                return BadRequest(result.LastMessage());

            return Ok(result.Data);
        }
    }
}
