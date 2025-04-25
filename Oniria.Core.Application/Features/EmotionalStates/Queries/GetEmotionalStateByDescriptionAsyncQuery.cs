using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetEmotionalStateByDescriptionAsyncQuery : IRequest<OperationResult<EmotionalStatesEntity>> 
    { 
        public string Description { get; set; }
    }

    public class GetEmotionalStateByDescriptionAsyncQueryHandler : IRequestHandler<GetEmotionalStateByDescriptionAsyncQuery, OperationResult<EmotionalStatesEntity>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;

        public GetEmotionalStateByDescriptionAsyncQueryHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<OperationResult<EmotionalStatesEntity>> Handle(GetEmotionalStateByDescriptionAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<EmotionalStatesEntity>.Create();
            var emotionalState = (await emotionalStatesRepository.GetAllAsync())
                .FirstOrDefault(es => es.Description == request.Description);

            if (emotionalState == null)
            {
                result.AddError("No such emotional state was found by description.");
            }
            else
            {
                result.Data = emotionalState;
            }

            return result;
        }
    }
}
