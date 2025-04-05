using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Response;
using Oniria.Core.Application.Extensions;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class GetUserSessionAsyncQuery : IRequest<OperationResult<UserResponse>> { }

    public class GetUserSessionAsyncQueryHandler : IRequestHandler<GetUserSessionAsyncQuery, OperationResult<UserResponse>>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string UserKeySession;

        public GetUserSessionAsyncQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.UserKeySession = config.GetSection("SessionConfig").GetValue<string>("UserInfoKey")!;
        }

        public Task<OperationResult<UserResponse>> Handle(GetUserSessionAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<UserResponse>.Create();

            result.Data = httpContextAccessor.HttpContext?.Session.Get<UserResponse>(UserKeySession);

            return Task.FromResult(result);
        }
    }
}
