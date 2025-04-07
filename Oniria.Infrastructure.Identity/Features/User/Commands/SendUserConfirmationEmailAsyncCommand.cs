using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Oniria.Core.Application.Extensions;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.Email.Request;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Shared.Features.Email.Commands;
using Oniria.Infrastructure.Shared.Features.Email.Queries;
using System.Text;

namespace Oniria.Infrastructure.Identity.Features.User.Commands
{
    public class SendUserConfirmationEmailAsyncCommand : IRequest<OperationResult>
    {
        public string Email { get; set; }
    }

    public class SendUserConfirmationEmailAsyncCommandHandler : IRequestHandler<SendUserConfirmationEmailAsyncCommand, OperationResult>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMediator mediator;

        public SendUserConfirmationEmailAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(SendUserConfirmationEmailAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                result.AddError("No user with this ID was found");
                return result;
            }

            var url = QueryHelpers.AddQueryString(
                QueryHelpers.AddQueryString(
                    $"{httpContextAccessor.GetOrigin()}/Auth/ConfirmUserEmail",
                    "userId",
                    user.Id
                ),
                "token",
                WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(await userManager.GenerateEmailConfirmationTokenAsync(user)))
            );

            var emailTemplateResult = await mediator.Send(new GetMailTemplateByNameAsyncQuery() { Name = "EmailConfirmation" });

            if (!emailTemplateResult.IsSuccess)
            {
                result.AddError("An error occurred obtaining the mail template for mail confirmation.");
                return result;
            }

            var sendEmailResult = await mediator.Send(new SendEmailAsyncCommand
            {
                Request = new EmailRequest
                {
                    To = user.Email!,
                    Subject = emailTemplateResult.Data!.Subject,
                    Body = emailTemplateResult.Data!.InsertUrl(url)
                }
            });

            if (!sendEmailResult.IsSuccess)
            {
                result.AddError("An error occurred while sending the confirmation email.");
                return result;
            }

            return result;
        }
    }
}
