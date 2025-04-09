using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Dream.Queries
{
    public class GetAllDreamAsyncQuery : IRequest<OperationResult<List<DreamEntity>>> { }

    public class GetAllDreamAsyncQueryHandler : IRequestHandler<GetAllDreamAsyncQuery, OperationResult<List<DreamEntity>>>
    {
        private readonly IDreamRepository dreamRepository;

        public GetAllDreamAsyncQueryHandler(IDreamRepository dreamRepository)
        {
            this.dreamRepository = dreamRepository;
        }

        public async Task<OperationResult<List<DreamEntity>>> Handle(GetAllDreamAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<DreamEntity>>.Create();
            result.Data = await dreamRepository.GetAllAsync();
            return result;
        }
    }
}
