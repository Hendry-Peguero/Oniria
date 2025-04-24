using MediatR;
using Oniria.Core.Application.Features.Base;

namespace Oniria.Core.Application.Features.User.Commands
{
    public class DeleteUserByIdAsyncCommand : IRequest<OperationResult>
    {
        public string UserId { get; set; }
    }
}
