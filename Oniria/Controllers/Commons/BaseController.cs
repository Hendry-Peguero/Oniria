using MediatR;
using Microsoft.AspNetCore.Mvc;
using Oniria.Services;

namespace Oniria.Controllers.Commons
{
    public class BaseController : Controller
    {
        private IMediator? _Mediator;
        protected IMediator Mediator => _Mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        private IUserContextService? _userContextService;
        protected IUserContextService UserContext => 
            _userContextService ??= HttpContext.RequestServices.GetService<IUserContextService>()!;
    }
}