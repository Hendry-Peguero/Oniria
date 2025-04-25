using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamAnalysis.Queries;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamAnalysis.Commands
{
    public class DeleteDreamAnalysisByIdAsyncCommand : IRequest<OperationResult>
    {
        public string Id { get; set; }
    }

    public class DeleteDreamAnalysisByIdAsyncCommandHandler : IRequestHandler<DeleteDreamAnalysisByIdAsyncCommand, OperationResult>
    {
        private readonly IDreamAnalysisRepository dreamAnalysisRepository;
        private readonly IMediator mediator;

        public DeleteDreamAnalysisByIdAsyncCommandHandler(
            IDreamAnalysisRepository dreamAnalysisRepository,
            IMediator mediator
        )
        {
            this.dreamAnalysisRepository = dreamAnalysisRepository;
            this.mediator = mediator;
        }

        public async Task<OperationResult> Handle(DeleteDreamAnalysisByIdAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create();

            var dreamAnalysisResult = await mediator.Send(new GetDreamAnalysisByIdAsyncQuery { Id = command.Id });

            if (!dreamAnalysisResult.IsSuccess)
            {
                result.AddError(dreamAnalysisResult);
                return result;
            }

            try
            {
                await dreamAnalysisRepository.DeleteAsync(dreamAnalysisResult.Data!);
            }
            catch (Exception ex)
            {
                result.AddError("Dream analysis could not be removed");
            }

            return result;
        }
    }
}
