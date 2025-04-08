using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamToken.Queries
{
    public class GetAllDreamTokensAsyncQuery : IRequest<OperationResult<List<DreamTokenEntity>>> { }

    public class GetAllDreamTokensAsyncQueryHandler : IRequestHandler<GetAllDreamTokensAsyncQuery, OperationResult<List<DreamTokenEntity>>>
    {
        private readonly IDreamTokenRepository _dreamTokenRepository;

        public GetAllDreamTokensAsyncQueryHandler(IDreamTokenRepository dreamTokenRepository)
        {
            _dreamTokenRepository = dreamTokenRepository;
        }

        public async Task<OperationResult<List<DreamTokenEntity>>> Handle(GetAllDreamTokensAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<DreamTokenEntity>>.Create();

            result.Data = await _dreamTokenRepository.GetAllAsync();

            return result;
        }
    }
}
