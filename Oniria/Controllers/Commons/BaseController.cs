using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Oniria.Services;

namespace Oniria.Controllers.Commons
{
    public class BaseController : Controller
    {
        // For Queries and Commands
        private IMediator? _Mediator;
        protected IMediator Mediator => _Mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        // For Get All the user info if is Logged
        private IUserContextService? _userContextService;
        protected IUserContextService UserContext => 
            _userContextService ??= HttpContext.RequestServices.GetService<IUserContextService>()!;

        // For Notifications
        private IToastNotification? _toastNotification;
        protected IToastNotification ToastNotification =>
            _toastNotification ??= HttpContext.RequestServices.GetService<IToastNotification>()!;
    }
}