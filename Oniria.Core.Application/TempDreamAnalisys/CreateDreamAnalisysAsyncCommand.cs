using AutoMapper;
using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamAnalysis;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Dtos.DreamAnalsys.Request;

namespace Oniria.Core.Application.TempDreamAnalysis
{
    public class CreateDreamAnalisysAsyncCommand : IRequest<OperationResult<DreamAnalysisEntity>>
    {
        public CreateDreamAnalysisRequest Request { get; set; }
    }

    public class CreateDreamAnalysisAsyncCommandHandler : IRequestHandler<CreateDreamAnalisysAsyncCommand, OperationResult<DreamAnalysisEntity>>
    {
        private readonly IDreamAnalysisRepository dreamAnalysisRepository;
        private readonly IMapper mapper;

        public CreateDreamAnalysisAsyncCommandHandler (IDreamAnalysisRepository dreamAnalysisRepository, IMapper mapper)
        {
            this.dreamAnalysisRepository = dreamAnalysisRepository;
            this.mapper = mapper;
        }

        public async Task<OperationResult<DreamAnalysisEntity>> Handle(CreateDreamAnalisysAsyncCommand command, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamAnalysisEntity>.Create();
            var request = command.Request;

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
