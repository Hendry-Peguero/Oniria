using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Interfaces.Repositories;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Features.Gender.Queries
{

    public class GetAllGenderAsyncQuery : IRequest<UseCaseResult<List<GenderEntity>>> { }

    public class GetAllGenderAsyncQueryHandler : IRequestHandler<GetAllGenderAsyncQuery, UseCaseResult<List<GenderEntity>>>
    {
        private readonly IGenderRepository genderRepository;

        public GetAllGenderAsyncQueryHandler(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository; 
        }

        public async Task<UseCaseResult<List<GenderEntity>>> Handle(GetAllGenderAsyncQuery request, CancellationToken cancellationToken)
        {
            // Crear Result
            var result = new UseCaseResult<List<GenderEntity>>();

            result.Data = await genderRepository.GetAllAsync();

            return result;
        }
    }
}
