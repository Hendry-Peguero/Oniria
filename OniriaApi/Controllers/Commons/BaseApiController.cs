using Microsoft.AspNetCore.Mvc;
using Oniria.Core.Application.Features.Base;

namespace WebApi.DreamHouse.Controllers.General
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediatorWrapper _mediator;
        protected IMediatorWrapper Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediatorWrapper>()!;
    }
}
