using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetAllAsyncEmotionalStatesQuery : IRequest<UseCaseResult<List<EmotionalStatesEntity>>> { }

    public class GetAllAsyncEmotionalStatesHandler : IRequestHandler<GetAllAsyncEmotionalStatesQuery, UseCaseResult<List<EmotionalStatesEntity>>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;

        public GetAllAsyncEmotionalStatesHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<UseCaseResult<List<EmotionalStatesEntity>>> Handle(GetAllAsyncEmotionalStatesQuery request, CancellationToken cancellationToken)
        {
            var result = new UseCaseResult<List<EmotionalStatesEntity>>();
            result.Data = await emotionalStatesRepository.GetAllAsync();
            return result;
        }
    }
}
