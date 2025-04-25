using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Application.Features.Patient.Commands;
using Oniria.Core.Application.Features.Patient.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.Patient.Request;
using WebApi.DreamHouse.Controllers.General;

namespace OniriaApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(ActorsRoles.DOCTOR))]
    public class PatientController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PatientEntity>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetAllPatientAsyncQuery());

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null || !result.Data.Any())
                return NoContent();

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await Mediator.Send(new GetPatientByIdAsyncQuery { Id = id });

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, result.LastMessage());

            if (result.Data == null)
                return NotFound();

            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
        {
            var result = await Mediator.Send(new CreatePatientAsyncCommand { Request = request });

            if (!result.IsSuccess)
                return BadRequest(result.LastMessage());

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdatePatientRequest request)
        {
            var result = await Mediator.Send(new UpdatePatientAsyncCommand { Request = request });

            if (!result.IsSuccess)
                return BadRequest(result.LastMessage());

            return Ok(result.Data);
        }
    }
}
