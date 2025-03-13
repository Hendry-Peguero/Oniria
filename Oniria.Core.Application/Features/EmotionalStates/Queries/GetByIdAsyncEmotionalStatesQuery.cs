using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.EmotionalStates.Queries
{
    public class GetByIdAsyncEmotionalStatesQuery : IRequest<UseCaseResult<EmotionalStatesEntity>>
    {
        public string Id { get; set; }
    }
    public class GetByIdAsyncEmotionalStatesQueryHandler : IRequestHandler<GetByIdAsyncEmotionalStatesQuery, UseCaseResult<EmotionalStatesEntity>>
    {
        private readonly IEmotionalStatesRepository emotionalStatesRepository;
        
        public GetByIdAsyncEmotionalStatesQueryHandler(IEmotionalStatesRepository emotionalStatesRepository)
        {
            this.emotionalStatesRepository = emotionalStatesRepository;
        }

        public async Task<UseCaseResult<EmotionalStatesEntity>> Handle(GetByIdAsyncEmotionalStatesQuery request, CancellationToken cancellationToken)
        {
            var result = new UseCaseResult<EmotionalStatesEntity>();
            result.Data = await emotionalStatesRepository.GetByIdAsync(request.Id);
            return result;
        }
    }
}
