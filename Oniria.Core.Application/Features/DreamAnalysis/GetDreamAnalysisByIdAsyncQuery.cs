using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Features.DreamAnalysis;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamAnalysis
{
    public class GetDreamAnalysisByIdAsyncQuery : IRequest<OperationResult<DreamAnalysisEntity>>
    {
        public string Id { get; set; }
    }
    public class GetDreamAnalisysByIdAsyncQueryHandler : IRequestHandler<GetDreamAnalysisByIdAsyncQuery, OperationResult<DreamAnalysisEntity>>
    {
        private readonly IDreamAnalysisRepository dreamAnalysisRepository;
        public GetDreamAnalisysByIdAsyncQueryHandler(IDreamAnalysisRepository dreamAnalysisRepository)
        {
            this.dreamAnalysisRepository = dreamAnalysisRepository;
        }
        public async Task<OperationResult<DreamAnalysisEntity>> Handle(GetDreamAnalysisByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamAnalysisEntity>.Create();
            var dreamAnalisys = await dreamAnalysisRepository.GetByIdAsync(request.Id);
            if (dreamAnalisys == null)
            {
                result.AddError("Could not get ID of dreamAnalisys");
            }
            else
            {
                result.Data = dreamAnalisys;
            }
            return result;
        }
    }
}
