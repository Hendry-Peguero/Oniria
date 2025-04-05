using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Oniria.Core.Application.Features.Base;
using Oniria.Infrastructure.Identity.Entities;
using System.Text;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class CreateConfirmationEmailUrlAsyncCommand : IRequest<OperationResult<string>>
    {
        public string UserId { get; set; }
    }

    public class CreateConfirmationEmailUrlAsyncCommandHandler : IRequestHandler<CreateConfirmationEmailUrlAsyncCommand, OperationResult<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CreateConfirmationEmailUrlAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<string>> Handle(CreateConfirmationEmailUrlAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<string>();
            var userResult = await userManager.FindByIdAsync(command.UserId);

            if (userResult == null)
            {
                result.AddError("No user with this ID was found");
                return result;
            }

            // Uri
            var path = "Authorization/ConfirmEmail";
            var origin = httpContextAccessor.HttpContext?.Request.Headers["Origin"].FirstOrDefault();
            var userParam = userResult.Id;
            var tokenParam = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(await userManager.GenerateEmailConfirmationTokenAsync(userResult))
            );

            result.Data = QueryHelpers.AddQueryString(
                QueryHelpers.AddQueryString(
                    new Uri($"{origin}/{path}").ToString(), 
                    "userId",
                    userParam
                ), 
                "token", 
                tokenParam
            );

            return result;
        }
    }
}
