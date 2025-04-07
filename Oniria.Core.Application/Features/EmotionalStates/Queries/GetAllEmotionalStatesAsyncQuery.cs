using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetAllEmotionalStatesAsyncQuery : IRequest<OperationResult<List<EmotionalStatesEntity>>> { }

    public class GetAllEmotionalStatesAsyncQueryHandler : IRequestHandler<GetAllEmotionalStatesAsyncQuery, OperationResult<List<EmotionalStatesEntity>>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;

        public GetAllEmotionalStatesAsyncQueryHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<OperationResult<List<EmotionalStatesEntity>>> Handle(GetAllEmotionalStatesAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<EmotionalStatesEntity>>.Create();
            result.Data = await emotionalStatesRepository.GetAllAsync();
            return result;
        }
    }
}
