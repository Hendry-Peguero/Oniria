using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Oniria.Controllers.Commons
{
    public class BaseController : Controller
    {
        private IMediator _Mediator;
        protected IMediator Mediator => _Mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}