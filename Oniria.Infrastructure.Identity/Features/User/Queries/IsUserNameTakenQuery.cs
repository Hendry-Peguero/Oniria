using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class IsUserNameTakenQuery : IRequest<OperationResult<bool>>
    {
        public string UserName { get; set; }
    }

    public class IsUserNameTakenQueryHandler : IRequestHandler<IsUserNameTakenQuery, OperationResult<bool>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IsUserNameTakenQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult<bool>> Handle(IsUserNameTakenQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<bool>.Create();

            var user = await userManager.FindByNameAsync(request.UserName);
            result.Data = user != null;

            return result;
        }
    }
}
