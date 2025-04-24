using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Application.Features.User.Commands
{
    public class AssignUserRoleAsyncCommand : IRequest<OperationResult>
    {
        public string UserId { get; set; }
        public ActorsRoles Role { get; set; }
    }
}
