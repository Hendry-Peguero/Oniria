using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamAnalysis
{
    public class GetAllDreamAnalysisAsyncQuery : IRequest<OperationResult<List<DreamAnalysisEntity>>> { }
    public class GetAllDreamAnalisysAsyncQueryHandler : IRequestHandler<GetAllDreamAnalysisAsyncQuery, OperationResult<List<DreamAnalysisEntity>>>
    {
        private readonly IDreamAnalysisRepository dreamAnalysisRepository;
        public GetAllDreamAnalisysAsyncQueryHandler(IDreamAnalysisRepository dreamAnalysisRepository)
        {
            this.dreamAnalysisRepository = dreamAnalysisRepository;
        }
        public async Task<OperationResult<List<DreamAnalysisEntity>>> Handle(GetAllDreamAnalysisAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<DreamAnalysisEntity>>.Create();
            result.Data = await dreamAnalysisRepository.GetAllAsync();
            return result;
        }
    }
}
