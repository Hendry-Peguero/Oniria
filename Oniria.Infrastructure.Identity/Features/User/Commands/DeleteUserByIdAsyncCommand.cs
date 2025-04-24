using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class DeleteUserByIdAsyncCommandHandler : IRequestHandler<DeleteUserByIdAsyncCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DeleteUserByIdAsyncCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult> Handle(DeleteUserByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                result.AddError("The user to be deleted was not found");
                return result;
            }

            try
            {
                await userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                result.AddError("An error occurred deleting the user");
            }

            return result;
        }
    }
}
