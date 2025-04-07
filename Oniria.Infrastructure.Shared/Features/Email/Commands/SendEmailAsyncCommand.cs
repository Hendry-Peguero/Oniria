using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.Email.Request;
using Oniria.Infrastructure.Shared.Entities;

namespace Oniria.Infrastructure.Shared.Features.Email.Commands
{
    public class SendEmailAsyncCommand : IRequest<OperationResult>
    {
        public EmailRequest Request { get; set; }
    }

    public class SendEmailAsyncCommandHandler : IRequestHandler<SendEmailAsyncCommand, OperationResult>
    {
        private readonly MailSettings mailSettings;

        public SendEmailAsyncCommandHandler(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public async Task<OperationResult> Handle(SendEmailAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse($"{mailSettings.DisplayName} <{mailSettings.EmailFrom}>");
                email.To.Add(MailboxAddress.Parse(command.Request.To));
                email.Subject = command.Request.Subject;

                var builder = new BodyBuilder { HtmlBody = command.Request.Body };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(mailSettings.SmtpHost, mailSettings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
                await smtp.AuthenticateAsync(mailSettings.SmtpUser, mailSettings.SmtpPassword, cancellationToken);
                await smtp.SendAsync(email, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
            }
            catch (Exception ex)
            {
                result.AddError($"An error occurred while sending the email");
            }

            return result;
        }
    }
}
