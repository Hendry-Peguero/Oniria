using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Constants;
using Oniria.Infrastructure.Identity.Entities;
using System.Text;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class ConfirmRestoreUserPasswordAsyncCommand : IRequest<OperationResult<string>>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class ConfirmRestoreUserPasswordAsyncCommandHandler : IRequestHandler<ConfirmRestoreUserPasswordAsyncCommand, OperationResult<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ConfirmRestoreUserPasswordAsyncCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult<string>> Handle(ConfirmRestoreUserPasswordAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = OperationResult<string>.Create();
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                result.AddError("No user with this ID was found");
                return result;
            }

            var newPassword = GeneratorHelper.RandomPassword(UserConstants.MIN_PASSOWRD_CHARACTERS);

            var resetResult = await userManager.ResetPasswordAsync(
                user,
                Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token)),
                newPassword
            );

            if (!resetResult.Succeeded)
            {
                result.AddError("Password could not be reset");
                return result;
            }

            result.Data = newPassword;

            return result;
        }
    }
}
