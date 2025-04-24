using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Request;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.User.Commands
{
    public class CreateUserAsyncCommand : IRequest<OperationResult<UserResponse>>
    {
        public CreateUserRequest Request { get; set; }
    }
}
