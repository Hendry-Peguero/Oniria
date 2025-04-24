using MediatR;
using Oniria.Core.Application.Features.Base;

namespace Oniria.Core.Application.Features.User.Commands
{
    public class SendUserConfirmationEmailAsyncCommand : IRequest<OperationResult>
    {
        public string Email { get; set; }
    }
}
