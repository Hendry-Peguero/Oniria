using Microsoft.AspNetCore.Mvc;
using Oniria.Controllers.Commons;
using Oniria.Infrastructure.Identity.Features.User.Commands;

namespace Oniria.Controllers
{
    public class AuthController : BaseController
    {
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
