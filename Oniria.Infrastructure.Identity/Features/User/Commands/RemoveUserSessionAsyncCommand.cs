using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Oniria.Core.Application.Features.Base;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class RemoveUserSessionAsyncCommand : IRequest<OperationResult> { }

    public class RemoveUserSessionAsyncCommandHandler : IRequestHandler<RemoveUserSessionAsyncCommand, OperationResult>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string UserKeySession;

        public RemoveUserSessionAsyncCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.UserKeySession = config.GetSection("SessionConfig").GetValue<string>("UserInfoKey")!;
        }

        public Task<OperationResult> Handle(RemoveUserSessionAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            try
            {
                httpContextAccessor.HttpContext?.Session.Remove(UserKeySession);
            }
            catch (Exception ex)
            {
                result.AddError("An error occurred removing the user from the session");
            }

            return Task.FromResult(result);
        }
    }
}
