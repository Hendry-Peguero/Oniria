using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.JWT;
using Oniria.Infrastructure.Identity.Features.JWT.Commands;

namespace Oniria.Infrastructure.Identity.Features.JWT.Queries
{
    public class GetRefreshJWTTokenAsyncQuery : IRequest<OperationResult<RefreshToken>> { }

    public class GetRefreshJWTTokenAsyncQueryHandler : IRequestHandler<GetRefreshJWTTokenAsyncQuery, OperationResult<RefreshToken>>
    {
        private readonly IMediator mediator;

        public GetRefreshJWTTokenAsyncQueryHandler(
            IMediator mediator
        )
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<RefreshToken>> Handle(GetRefreshJWTTokenAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<RefreshToken>.Create();

            var refreshToken = await mediator.Send(new CreateRefreshTokenCommand());

            if (!refreshToken.IsSuccess)
            {
                result.AddError("Error generating refresh token");
                return result;
            }

            result.Data = refreshToken.Data;

            return result;
        }
    }
}
