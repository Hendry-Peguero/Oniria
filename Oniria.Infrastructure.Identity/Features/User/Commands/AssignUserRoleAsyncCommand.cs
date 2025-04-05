using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Enums;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class AssignUserRoleAsyncCommand : IRequest<OperationResult>
    {
        public ApplicationUser User { get; set; }
        public ActorsRoles Role { get; set; }
    }

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

            if (await userManager.IsInRoleAsync(command.User, command.Role.ToString()))
            {
                result.AddError("The user already has this role");
                return result;
            }

            var addRoleResult = await userManager.AddToRoleAsync(command.User, command.Role.ToString());

            if (!addRoleResult.Succeeded)
            {
                result.AddError("An error occurred assigning the role to the user");
            }

            return result;
        }
    }
}
