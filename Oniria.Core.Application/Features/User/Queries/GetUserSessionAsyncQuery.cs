using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.User.Response;

namespace Oniria.Core.Application.Features.User.Queries
{
    public class GetUserSessionAsyncQuery : IRequest<OperationResult<UserResponse>> { }
}
