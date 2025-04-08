using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

            try
            {
                var tokens = await _dreamTokenRepository.GetAllAsync();
                result.Data = tokens;
            }
            catch (System.Exception ex)
            {
                result.AddError("Error al obtener DreamTokens: " + ex.Message);
            }

            return result;
        }
    }
}
