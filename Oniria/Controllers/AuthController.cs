using Microsoft.AspNetCore.Mvc;
using Oniria.Attributes;
using Oniria.Controllers.Commons;
using Oniria.Core.Dtos.User.Request;
using Oniria.Helpers;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using Oniria.ViewModels.Auth;

namespace Oniria.Controllers
{
    public class AuthController : BaseController
    {
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public IActionResult Login() => View();


        [HttpPost]
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var loginResult = await Mediator.Send(new LoginUserAsyncCommand { Request = Mapper.Map<AuthenticationRequest>(model) });

            if (!loginResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, loginResult.LastMessage());
                return View(model);
            }

            return Redirections.HomeRedirection;
        }


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public IActionResult RegisterType() => View();


        [GoHome(GoHomeWhen.USER_OUT_SESSION)]
        public new async Task<IActionResult> SignOut()
        {
            var singOutResult = await Mediator.Send(new SignOutUserAsyncCommand());

            if (!singOutResult.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage(singOutResult.LastMessage());
                return Redirections.HomeRedirection;
            }

            var removeSession = await Mediator.Send(new RemoveUserSessionAsyncCommand());

            if (!removeSession.IsSuccess)
            {
                ToastNotification.AddErrorToastMessage(removeSession.LastMessage());
            }

            return Redirections.HomeRedirection;
        }


        public async Task<IActionResult> ConfirmUserEmail(string userId, string token)
        {
            var confirmResult = await Mediator.Send(new ConfirmUserEmailAsyncCommand
            {
                UserId = userId,
                Token = token
            });

            if (!confirmResult.IsSuccess) return Redirections.Unauthorized;

            return View("Confirmations/_EmailConfirmation");
        }


        public async Task<IActionResult> ConfirmUserPasswordRestore(string userId, string token)
        {
            var confirmResult = await Mediator.Send(new ConfirmRestoreUserPasswordAsyncCommand
            {
                UserId = userId,
                Token = token
            });

            if (!confirmResult.IsSuccess) return Redirections.Unauthorized;

            return View("Confirmations/_ResetPasswordConfirmation", confirmResult.Data!);
        }
    }
}
