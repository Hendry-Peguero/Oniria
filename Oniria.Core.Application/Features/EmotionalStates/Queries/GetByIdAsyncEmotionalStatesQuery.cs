using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetByIdAsyncEmotionalStatesQuery : IRequest<OperationResult<EmotionalStatesEntity>>
    {
        public string Id { get; set; }
    }
    public class GetByIdAsyncEmotionalStatesQueryHandler : IRequestHandler<GetByIdAsyncEmotionalStatesQuery, OperationResult<EmotionalStatesEntity>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;
        
        public GetByIdAsyncEmotionalStatesQueryHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<OperationResult<EmotionalStatesEntity>> Handle(GetByIdAsyncEmotionalStatesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<EmotionalStatesEntity>();
            result.Data = await emotionalStatesRepository.GetByIdAsync(request.Id);
            return result;
        }
    }
}
