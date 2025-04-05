using MediatR;
using Oniria.Core.Application.Features.Base;

namespace Oniria.Infrastructure.Identity.Features.User.Queries
{
    public class IsUserInSessionAsyncQuery : IRequest<OperationResult<bool>> { }

    public class IsUserInSessionAsyncQueryHandler : IRequestHandler<IsUserInSessionAsyncQuery, OperationResult<bool>>
    {
        private readonly IMediator mediator;

        public IsUserInSessionAsyncQueryHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<OperationResult<bool>> Handle(IsUserInSessionAsyncQuery command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<bool>();

            result.Data = (await mediator.Send(new GetUserSessionAsyncQuery())).Data != null;

            return result;
        }
    }
}
