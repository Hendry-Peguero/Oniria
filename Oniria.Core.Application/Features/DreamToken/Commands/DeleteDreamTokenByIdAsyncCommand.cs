using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamToken.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamToken.Commands
{
    public class DeleteDreamTokenByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteDreamTokenByIdAsyncCommandHandler : IRequestHandler<DeleteDreamTokenByIdAsyncCommand, OperationResult>
    {
        private readonly IDreamTokenRepository dreamTokenRepository;
        private readonly IMediator mediator;

        public DeleteDreamTokenByIdAsyncCommandHandler(
            IDreamTokenRepository dreamTokenRepository,
            IMediator mediator
        )
        {
            this.dreamTokenRepository = dreamTokenRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteDreamTokenByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            var dreamTokenResult = await mediator.Send(new GetDreamTokenByIdAsyncQuery { Id = command.Id });

            if (!dreamTokenResult.IsSuccess)
            {
                result.AddError(dreamTokenResult);
                return result;
            }

            try
            {
                await dreamTokenRepository.DeleteAsync(dreamTokenResult.Data!);
            }
            catch (Exception ex)
            {
                result.AddError("Dream Token could not be removed");
            }

            return result;
        }
    }
}
