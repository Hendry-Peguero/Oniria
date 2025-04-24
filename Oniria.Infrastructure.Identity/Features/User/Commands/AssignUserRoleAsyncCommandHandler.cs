using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Commands;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class AssignUserRoleAsyncCommandHandler : IRequestHandler<AssignUserRoleAsyncCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AssignUserRoleAsyncCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult> Handle(AssignUserRoleAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var user = await userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                result.AddError("No user with this ID was found.");
                return result;
            }

            if (await userManager.IsInRoleAsync(user, command.Role.ToString()))
            {
                result.AddError("The user already has this role");
                return result;
            }

            var addRoleResult = await userManager.AddToRoleAsync(user, command.Role.ToString());

            if (!addRoleResult.Succeeded)
            {
                result.AddError("An error occurred assigning the role to the user");
            }

            return result;
        }
    }
}
