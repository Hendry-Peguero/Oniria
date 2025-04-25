using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Dream.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Dream.Command
{
    public class DeleteDreamByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteDreamByIdAsyncCommandHandler : IRequestHandler<DeleteDreamByIdAsyncCommand, OperationResult>
    {
        private readonly IDreamRepository dreamRepository;
        private readonly IMediator mediator;

        public DeleteDreamByIdAsyncCommandHandler(
            IDreamRepository dreamRepository,
            IMediator mediator
        )
        {
            this.dreamRepository = dreamRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteDreamByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            var dreamResult = await mediator.Send(new GetDreamByIdAsyncQuery { Id = command.Id });

            if (!dreamResult.IsSuccess)
            {
                result.AddError(dreamResult);
                return result;
            }

            try
            {
                await dreamRepository.DeleteAsync(dreamResult.Data!);
            }
            catch (Exception ex)
            {
                result.AddError("Dream could not be removed");
            }

            return result;
        }
    }
}
