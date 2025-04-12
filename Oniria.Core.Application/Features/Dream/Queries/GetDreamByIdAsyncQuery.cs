using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Dream.Queries
{
    public class GetDreamByIdAsyncQuery : IRequest<OperationResult<DreamEntity>>
    {
        public string Id { get; set; }
    }

    public class GetDreamByIdAsyncQueryHandler : IRequestHandler<GetDreamByIdAsyncQuery, OperationResult<DreamEntity>>
    {
        private readonly IDreamRepository dreamRepository;

        public GetDreamByIdAsyncQueryHandler(IDreamRepository dreamRepository)
        {
            this.dreamRepository = dreamRepository;
        }

        public async Task<OperationResult<DreamEntity>> Handle(GetDreamByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<DreamEntity>.Create();
            var dream = await dreamRepository.GetByIdAsync(request.Id);

            if (dream != null)
            {
                result.Data = dream;
            }
            else
            {
                result.AddError("No such dream found by ID");
            }

            return result;
        }
    }
}
