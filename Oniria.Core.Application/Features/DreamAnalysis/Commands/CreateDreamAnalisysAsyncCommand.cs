using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.Dream.Queries;
using Oniria.Core.Application.Features.EmotionalStates.Queries;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.DreamAnalsys.Request;

namespace Oniria.Core.Application.Features.DreamAnalysis.Commands
{
    public class CreateDreamAnalisysAsyncCommand : IRequest<OperationResult<DreamAnalysisEntity>>
    {
        public CreateDreamAnalysisRequest Request { get; set; }
    }

    public class CreateDreamAnalysisAsyncCommandHandler : IRequestHandler<CreateDreamAnalisysAsyncCommand, OperationResult<DreamAnalysisEntity>>
    {
        private readonly IDreamAnalysisRepository dreamAnalysisRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;


        public CreateDreamAnalysisAsyncCommandHandler(
            IDreamAnalysisRepository dreamAnalysisRepository, 
            IMapper mapper,
            IMediator mediator
        )
        {
            this.dreamAnalysisRepository = dreamAnalysisRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }


        public async Task<OperationResult<DreamAnalysisEntity>> Handle(CreateDreamAnalisysAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamAnalysisEntity>.Create();
            var request = command.Request;

            var emotionalStatesResult = await mediator.Send(new GetEmotionalStatesByIdAsyncQuery { Id = request.EmotionalStateId });

            if (!emotionalStatesResult.IsSuccess)
            {
                result.AddError(emotionalStatesResult);
                return result;
            }

            var dreamResult = await mediator.Send(new GetDreamByIdAsyncQuery { Id = request.DreamId });

            if (!dreamResult.IsSuccess)
            {
                result.AddError(dreamResult);
                return result;
            }

            var dreamAnalysisToCreate = mapper.Map<DreamAnalysisEntity>(request);

            try
            {
                await dreamAnalysisRepository.CreateAsync(dreamAnalysisToCreate);
            }
            catch (Exception ex)
            {
                result.AddError("DreamAnlasys could not be completed");
                return result;
            }

            result.Data = dreamAnalysisToCreate;

            return result;
        }
    }
}
