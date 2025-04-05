using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Oniria.Core.Application.Features.Base;
using Oniria.Infrastructure.Identity.Entities;
using System.Text;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class ConfirmUserEmailAsyncCommand : IRequest<OperationResult>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class ConfirmUserEmailAsyncCommandHandler : IRequestHandler<ConfirmUserEmailAsyncCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ConfirmUserEmailAsyncCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult> Handle(ConfirmUserEmailAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                result.AddError("There is no user with this id");
                return result;
            }

            // Confirm the user's email
            var confirmResult = await userManager.ConfirmEmailAsync(
                user,
                Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(command.Token))
            );

            if (!confirmResult.Succeeded)
            {
                result.AddError("This user's email could not be confirmed");
            }

            return result;
        }
    }
}
