using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Response;
using Oniria.Core.Application.Extensions;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class SetUserSessionAsyncCommand : IRequest<OperationResult>
    {
        public UserResponse User { get; set; }
    }

    public class SetUserSessionAsyncCommandHandler : IRequestHandler<SetUserSessionAsyncCommand, OperationResult>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string UserKeySession;

        public SetUserSessionAsyncCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration config
        )
        {
            this.httpContextAccessor = httpContextAccessor;
            this.UserKeySession = config.GetSection("SessionConfig").GetValue<string>("UserInfoKey")!;
        }

        public Task<OperationResult> Handle(SetUserSessionAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            try
            {
                httpContextAccessor.HttpContext!.Session.Set(UserKeySession, command.User);
            }
            catch(Exception ex)
            {
                result.AddError("An error occurred while setting the user session.");
            }

            return Task.FromResult(result);
        }
    }
}
