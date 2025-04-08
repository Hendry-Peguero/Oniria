using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.DreamToken.Queries
{
    public class GetDreamTokenByIdAsyncQuery : IRequest<OperationResult<DreamTokenEntity>>
    {
        public string Id { get; set; }
    }

    public class GetDreamTokenByIdAsyncQueryHandler : IRequestHandler<GetDreamTokenByIdAsyncQuery, OperationResult<DreamTokenEntity>>
    {
        private readonly IDreamTokenRepository _dreamTokenRepository;

        public GetDreamTokenByIdAsyncQueryHandler(IDreamTokenRepository dreamTokenRepository)
        {
            _dreamTokenRepository = dreamTokenRepository;
        }

        public async Task<OperationResult<DreamTokenEntity>> Handle(GetDreamTokenByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamTokenEntity>.Create();

            var token = await _dreamTokenRepository.GetByIdAsync(request.Id);

            if (token == null)
            {
                result.AddError("DreamToken not found");
            }
            else
            {
                result.Data = token;
            }

            return result;
        }
    }
}
