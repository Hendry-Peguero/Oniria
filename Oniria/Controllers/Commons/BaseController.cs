using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Oniria.Core.Application.Features.Base;
using Oniria.Services;

namespace Oniria.Controllers.Commons
{
    public class BaseController : Controller
    {
        // For Queries and Commands
        private IMediatorWrapper? _Mediator;
        protected IMediatorWrapper Mediator => _Mediator ??= HttpContext.RequestServices.GetService<IMediatorWrapper>()!;

        // For Mappings
        private IMapper? _mapper;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>()!;

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