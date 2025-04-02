using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Enums;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class ChangeUserStatusAsyncCommand : IRequest<OperationResult>
    {
        public string UserId { get; set; }
        public StatusEntity Status { get; set; }
    }

    public class ChangeUserStatusAsyncCommandHandler : IRequestHandler<ChangeUserStatusAsyncCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ChangeUserStatusAsyncCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult> Handle(ChangeUserStatusAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                result.AddError("There is no user with this id");
                return result;
            }

            user.Status = command.Status;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                result.AddError("Error updating user status");
            }

            return result;
        }
    }
}
