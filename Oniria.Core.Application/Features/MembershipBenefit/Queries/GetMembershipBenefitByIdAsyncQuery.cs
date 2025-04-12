using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipBenefit.Queries
{
    public class GetMembershipBenefitByIdAsyncQuery : IRequest<OperationResult<MembershipBenefitEntity>>
    {
        public string Id { get; set; }
    }

    public class GetMembershipBenefitByIdAsyncQueryHandler : IRequestHandler<GetMembershipBenefitByIdAsyncQuery, OperationResult<MembershipBenefitEntity>>
    {
        private readonly IMembershipBenefitRepository membershipBenefitRepository;

        public GetMembershipBenefitByIdAsyncQueryHandler(IMembershipBenefitRepository membershipBenefitRepository)
        {
            this.membershipBenefitRepository = membershipBenefitRepository;
        }

        public async Task<OperationResult<MembershipBenefitEntity>> Handle(GetMembershipBenefitByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipBenefitEntity>.Create();
            var membershipBenefit = await membershipBenefitRepository.GetByIdAsync(request.Id);

            if (membershipBenefit == null)
            {
                result.AddError("Could not obtain the membership benefit by ID");
            }
            else
            {
                result.Data = membershipBenefit;
            }

            return result;
        }
    }
}
