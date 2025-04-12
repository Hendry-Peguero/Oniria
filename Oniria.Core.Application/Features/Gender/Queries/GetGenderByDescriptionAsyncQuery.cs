using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Gender.Queries
{
    public class GetGenderByDescriptionAsyncQuery : IRequest<OperationResult<GenderEntity>>
    {
        public string Description { get; set; }
    }

    public class GetGenderByDescriptionAsyncQueryHandler : IRequestHandler<GetGenderByDescriptionAsyncQuery, OperationResult<GenderEntity>>
    {
        private readonly IGenderRepository genderRepository;

        public GetGenderByDescriptionAsyncQueryHandler(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository;
        }

        public async Task<OperationResult<GenderEntity>> Handle(GetGenderByDescriptionAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<GenderEntity>.Create();
            var gender = (await genderRepository.GetAllAsync()).FirstOrDefault(g => g.Description == request.Description);

            if (gender != null)
            {
                result.Data = gender;
            }
            else
            {
                result.AddError($"There is no gender with this description [{request.Description}]");
            }

            return result;
        }
    }
}
