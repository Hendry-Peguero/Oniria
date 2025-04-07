using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.User.Queries;
using Oniria.Core.Dtos.User.Response;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class GetUserByIdAsyncQueryHandler : IRequestHandler<GetUserByIdAsyncQuery, OperationResult<UserResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public GetUserByIdAsyncQueryHandler(
            UserManager<ApplicationUser> userManager, 
            IMapper mapper,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<OperationResult<UserResponse>> Handle(GetUserByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<UserResponse>.Create();
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                result.AddError("No user with this ID was found.");
                return result;
            }

            var userResponse = mapper.Map<UserResponse>(user);
            userResponse.Roles = (await mediator.Send(new GetAllUserRolesAsyncQuery { User = user })).Data;

            result.Data = userResponse;

            return result;
        }
    }
}
