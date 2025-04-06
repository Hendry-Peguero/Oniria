using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.Gender.Queries
{

    public class GetAllGenderAsyncQuery : IRequest<OperationResult<List<GenderEntity>>> { }

    public class GetAllGenderAsyncQueryHandler : IRequestHandler<GetAllGenderAsyncQuery, OperationResult<List<GenderEntity>>>
    {
        private readonly IGenderRepository genderRepository;

        public GetAllGenderAsyncQueryHandler(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository; 
        }

        public async Task<OperationResult<List<GenderEntity>>> Handle(GetAllGenderAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<GenderEntity>>.Create();

            result.Data = await genderRepository.GetAllAsync();

            return result;
        }
    }
}
