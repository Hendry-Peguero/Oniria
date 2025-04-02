using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Enums;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class GetAllUserRolesAsyncQuery : IRequest<OperationResult<List<ActorsRoles>>>
    {
        public ApplicationUser User { get; set; }
    }

    public class GetAllUserRolesAsyncQueryHandler : IRequestHandler<GetAllUserRolesAsyncQuery, OperationResult<List<ActorsRoles>>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetAllUserRolesAsyncQueryHandler(
            UserManager<ApplicationUser> userManager
        )
        {
            this.userManager = userManager;
        }

        public async Task<OperationResult<List<ActorsRoles>>> Handle(GetAllUserRolesAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<List<ActorsRoles>>();
            var roles = await userManager.GetRolesAsync(request.User).ConfigureAwait(false);

            foreach (var rol in roles)
            {
                result.Data!.Add(Enum.Parse<ActorsRoles>(rol));
            }

            return result;
        }
    }
}
