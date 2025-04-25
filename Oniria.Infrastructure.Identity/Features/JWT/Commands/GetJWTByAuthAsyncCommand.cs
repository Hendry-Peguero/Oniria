using MediatR;
using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.JWT.Commands;
using Oniria.Core.Dtos.JWT.Response;
using Oniria.Core.Dtos.User.Request;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Identity.Features.JWT.Queries;
using Oniria.Infrastructure.Identity.Features.User.Commands;
using System.IdentityModel.Tokens.Jwt;

namespace Oniria.Infrastructure.Identity.Features.JWT.Commands
{
    public class GetJWTByAuthAsyncCommand : IRequest<OperationResult<JWTTokenResponse>>
    {
        public AuthenticationRequest Request { get; set; }
    }

    public class GetJWTByAuthAsyncCommandHandler : IRequestHandler<GetJWTByAuthAsyncCommand, OperationResult<JWTTokenResponse>>
    {
        private readonly IMediatorWrapper mediator;
        private readonly UserManager<ApplicationUser> userManager;

        public GetJWTByAuthAsyncCommandHandler(
            IMediatorWrapper mediator,
            UserManager<ApplicationUser> userManager
        )
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        public async Task<OperationResult<JWTTokenResponse>> Handle(GetJWTByAuthAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = OperationResult<JWTTokenResponse>.Create();
            var resultAuth = await mediator.Send(new LoginUserWithoutSessionAsyncCommand { Request = request.Request });

            if (!resultAuth.IsSuccess)
            {
                result.AddError(resultAuth);
                return result;
            }

            var userApp = await userManager.FindByIdAsync(resultAuth.Data!.Id);

            if (userApp is null)
            {
                result.AddError("Not user found");
                return result;
            }

            var tokenResult = await mediator.Send(new GetJWTTokenByUserAsyncQuery { User = userApp });

            if (!tokenResult.IsSuccess)
            {
                result.AddError(tokenResult);
                return result;
            }

            result.Data = tokenResult.Data!;

            return result;
        }
    }
}
