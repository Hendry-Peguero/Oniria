using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Gender.Queries
{

    public class GetGenderByIdAsyncQuery : IRequest<OperationResult<GenderEntity>>
    {
        public string Id { get; set; }
    }

    public class GetGenderByIdAsyncQueryHandler : IRequestHandler<GetGenderByIdAsyncQuery, OperationResult<GenderEntity>>
    {
        private readonly IGenderRepository genderRepository;

        public GetGenderByIdAsyncQueryHandler(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository;
        }

        public async Task<OperationResult<GenderEntity>> Handle(GetGenderByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<GenderEntity>.Create();
            var gender = await genderRepository.GetByIdAsync(request.Id);

            if (gender == null)
            {
                result.AddError("Could not obtain the gender by ID");
            }
            else
            {
                result.Data = gender;
            }

            return result;
        }
    }
}
