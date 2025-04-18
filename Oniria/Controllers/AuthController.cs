using Microsoft.AspNetCore.Mvc;
using Oniria.Attributes;
using Oniria.Controllers.Commons;
using Oniria.Helpers;
using Oniria.Infrastructure.Identity.Features.User.Commands;

namespace Oniria.Controllers
{
    public class AuthController : BaseController
    {
        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> Login() => View();



        //[HttpPost]
        //[GoHome(GoHomeWhen.USER_IN_SESSION)]
        //public async Task<IActionResult> Login(string temp)
        //{
        //    return Redirections.HomeRedirection;
        //}


        [GoHome(GoHomeWhen.USER_IN_SESSION)]
        public async Task<IActionResult> RegisterType()
        {
            return View();
        }


        [GoHome(GoHomeWhen.USER_OUT_SESSION)]
        public async Task<IActionResult> SignOut()
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

            if (!confirmResult.IsSuccess)
            {
                return View("HttpResponses/_401");
            }

            return View("Confirmations/_EmailConfirmation");
        }


        public async Task<IActionResult> ConfirmUserPasswordRestore(string userId, string token)
        {
            var confirmResult = await Mediator.Send(new ConfirmRestoreUserPasswordAsyncCommand
            {
                UserId = userId,
                Token = token
            });

            if (!confirmResult.IsSuccess)
            {
                return View("HttpResponses/_401");
            }

            return View("Confirmations/_ResetPasswordConfirmation", confirmResult.Data!);
        }
    }
}
