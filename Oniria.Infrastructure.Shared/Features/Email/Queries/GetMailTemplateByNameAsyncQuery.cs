using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Helpers;
using Oniria.Infrastructure.Shared.Entities;

namespace Oniria.Infrastructure.Shared.Features.Email.Queries
{
    public class GetMailTemplateByNameAsyncQuery : IRequest<OperationResult<MailTemplate>>
    {
        public string Name { get; set; }
    }

    public class GetMailTemplateByNameAsyncQueryHandler : IRequestHandler<GetMailTemplateByNameAsyncQuery, OperationResult<MailTemplate>>
    {
        private readonly string TemplateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/EmailTemplates.json");

        public async Task<OperationResult<MailTemplate>> Handle(GetMailTemplateByNameAsyncQuery command, CancellationToken cancellationToken)
        {
            var result = OperationResult<MailTemplate>.Create();

            try
            {
                if (!File.Exists(TemplateFilePath))
                {
                    result.AddError($"Template file not found at path '{TemplateFilePath}'");
                    return result;
                }

                var json = await File.ReadAllTextAsync(TemplateFilePath, cancellationToken);
                var templates = JsonHelper.Deserialize<Dictionary<string, MailTemplate>>(json);

                if (templates == null || !templates.ContainsKey(command.Name))
                {
                    result.AddError($"Template '{command.Name}' not found.");
                    return result;
                }

                result.Data = templates[command.Name];
            }
            catch (Exception)
            {
                result.AddError("An error occurred while getting the email template.");
            }

            return result;
        }
    }
}
