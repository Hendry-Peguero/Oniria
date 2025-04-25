using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Dtos.User.Request;
using Oniria.Infrastructure.Identity.Features.JWT.Commands;
using WebApi.DreamHouse.Controllers.General;

namespace OniriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("auth")]
        public async Task<IActionResult> AuthAsync(AuthenticationRequest request)
        {
            var authResult = await Mediator.Send(new GetJWTByAuthAsyncCommand { Request = request });

            if (!authResult.IsSuccess)
            {
                return BadRequest(authResult.LastMessage());
            }

            return Ok(authResult.Data!);
        }
    }
}
