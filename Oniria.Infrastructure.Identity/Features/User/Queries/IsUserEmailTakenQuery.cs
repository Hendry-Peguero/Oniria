using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class IsUserEmailTakenQuery : IRequest<OperationResult<bool>>
    {
        public string UserEmail { get; set; }
    }

    public class IsUserEmailTakenQueryHandler : IRequestHandler<IsUserEmailTakenQuery, OperationResult<bool>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IsUserEmailTakenQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult<bool>> Handle(
            IsUserEmailTakenQuery request,
            CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<bool>();

            var user = await userManager.FindByEmailAsync(request.UserEmail);
            result.Data = user != null;

            return result;
        }
    }
}
