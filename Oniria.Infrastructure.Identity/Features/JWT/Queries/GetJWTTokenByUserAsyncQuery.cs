using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.JWT.Commands;
using Oniria.Core.Dtos.JWT.Response;
using Oniria.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Oniria.Infrastructure.Identity.Features.JWT.Queries
{
    public class GetJWTTokenByUserAsyncQuery : IRequest<OperationResult<JWTTokenResponse>>
    {
        public ApplicationUser User { get; set; }
    }

    public class GetJWTTokenByUserAsyncQueryHandler : IRequestHandler<GetJWTTokenByUserAsyncQuery, OperationResult<JWTTokenResponse>>
    {
        private readonly IMediator mediator;

        public GetJWTTokenByUserAsyncQueryHandler(
            IMediator mediator
        )
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<JWTTokenResponse>> Handle(GetJWTTokenByUserAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<JWTTokenResponse>();

            var jwtResult = await mediator.Send(new CreateJWTTokenAsyncCommand
            {
                User = request.User
            });

            if (!jwtResult.IsSuccess)
            {
                result.AddError("Error generating JWT token");
                return result;
            }

            result.Data = new JWTTokenResponse
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtResult.Data)
            };

            return result;
        }
    }
}
