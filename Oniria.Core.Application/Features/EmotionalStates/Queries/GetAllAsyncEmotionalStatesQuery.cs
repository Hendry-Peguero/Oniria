using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetAllAsyncEmotionalStatesQuery : IRequest<OperationResult<List<EmotionalStatesEntity>>> { }

    public class GetAllAsyncEmotionalStatesHandler : IRequestHandler<GetAllAsyncEmotionalStatesQuery, OperationResult<List<EmotionalStatesEntity>>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;

        public GetAllAsyncEmotionalStatesHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<OperationResult<List<EmotionalStatesEntity>>> Handle(GetAllAsyncEmotionalStatesQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<EmotionalStatesEntity>>.Create();
            result.Data = await emotionalStatesRepository.GetAllAsync();
            return result;
        }
    }
}
