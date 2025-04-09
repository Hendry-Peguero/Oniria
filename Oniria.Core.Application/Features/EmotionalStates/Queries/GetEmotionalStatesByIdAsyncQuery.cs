using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetEmotionalStatesByIdAsyncQuery : IRequest<OperationResult<EmotionalStatesEntity>>
    {
        public string Id { get; set; }
    }

    public class GetEmotionalStatesByIdAsyncQueryHandler : IRequestHandler<GetEmotionalStatesByIdAsyncQuery, OperationResult<EmotionalStatesEntity>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;
        
        public GetEmotionalStatesByIdAsyncQueryHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<OperationResult<EmotionalStatesEntity>> Handle(GetEmotionalStatesByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<EmotionalStatesEntity>.Create();
            var emotionalState = await emotionalStatesRepository.GetByIdAsync(request.Id);

            if (emotionalState == null)
            {
                result.AddError("Emotional State not found");
            }
            else
            {
                result.Data = emotionalState;
            }

            return result;
        }
    }
}
