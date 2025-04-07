using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Core.Application.Features.Gender.Queries;
using Oniria.Core.Domain.Enums;
using Oniria.Core.Dtos.User.Request;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using Oniria.Infrastructure.Identity.Features.User.Queries;

namespace Oniria.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper mapper;

        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var resultGenres = await Mediator.Send(new GetAllGenderAsyncQuery());
            var resultUsers = await Mediator.Send(new GetAllUsersAsyncQuery());
            var resultUser = await Mediator.Send(new GetUserByIdAsyncQuery() { UserId = "1ok2l-vxztp-yub64-qm7fr-1298z" });

            ViewBag.Genres = resultGenres.Data;
            ViewBag.Users = resultUsers.Data;
            ViewBag.User = resultUser.Data;

            return View();
        }

        public async Task<IActionResult> GetUserSession()
        {
            return Json(await Mediator.Send(new GetUserSessionAsyncQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var loginResult = await Mediator.Send(new LoginUserAsyncCommand
            {
                Request = new AuthenticationRequest
                {
                    Identifier = "moises",
                    Password = "272EZcW.H5S38sh"
                }
            });

            if (!loginResult.IsSuccess)
            {
                return Json(loginResult);
            }

            var sessionResult = await Mediator.Send(new SetUserSessionAsyncCommand
            {
                User = loginResult.Data!
            });

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            var createResult = await Mediator.Send(new CreateUserAsyncCommand
            {
                Request = new CreateUserRequest
                {
                    Email = "moisesgironarias@gmail.com",
                    UserName = "moises",
                    Password = "123Pa$word!"
                }
            });

            if (!createResult.IsSuccess) return Json(createResult);

            var sendEmailResult = await Mediator.Send(new SendUserConfirmationEmailAsyncCommand
            {
                Email = createResult.Data!.Email
            });

            if (!sendEmailResult.IsSuccess) return Json(sendEmailResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser()
        {
            var editResult = await Mediator.Send(new UpdateUserAsyncCommand
            {
                Request = new UpdateUserRequest
                {
                    Id = "1229c1d0-9ae5-43a7-8f69-aebf09b557e8",
                    Email = "moisesgironarias@gmail.com",
                    UserName = "moises",
                    Password = "123Pa$word!",
                    Status = StatusEntity.ACTIVE
                }
            });

            if (!editResult.IsSuccess) return Json(editResult);

            return Json(new { ok = true });
        }

        [HttpPost]
        public async Task<IActionResult> RestorePassword()
        {
            var restoreResult = await Mediator.Send(new SendUserPasswordResetEmailAsyncCommand
            {
                Email = "moisesgironarias@gmail.com"
            });

            if (!restoreResult.IsSuccess) return Json(restoreResult);

            return Json(new { ok = true });
        }
    }
}
