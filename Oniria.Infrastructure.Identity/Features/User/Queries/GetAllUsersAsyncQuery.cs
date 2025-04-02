using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class GetAllUsersAsyncQuery : IRequest<OperationResult<List<UserResponse>>> { }

    public class GetAllUsersAsyncQueryHandler : IRequestHandler<GetAllUsersAsyncQuery, OperationResult<List<UserResponse>>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public GetAllUsersAsyncQueryHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<OperationResult<List<UserResponse>>> Handle(GetAllUsersAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<List<UserResponse>>();
            var users = userManager.Users.ToList();

            foreach (var user in users)
            {
                var userResponse = mapper.Map<UserResponse>(user);

                userResponse.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = user })).Data;
                result.Data!.Add(userResponse);
            }

            return result;
        }
    }
}
