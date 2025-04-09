using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipBenefit.Queries
{
    public class GetAllMembershipBenefitAsyncQuery : IRequest<OperationResult<List<MembershipBenefitEntity>>> { }

    public class GetAllMembershipBenefitAsyncQueryHandler : IRequestHandler<GetAllMembershipBenefitAsyncQuery, OperationResult<List<MembershipBenefitEntity>>>
    {
        private readonly IMembershipBenefitRepository membershipBenefitRepository;

        public GetAllMembershipBenefitAsyncQueryHandler(IMembershipBenefitRepository membershipBenefitRepository)
        {
            this.membershipBenefitRepository = membershipBenefitRepository;
        }

        public async Task<OperationResult<List<MembershipBenefitEntity>>> Handle(GetAllMembershipBenefitAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<MembershipBenefitEntity>>.Create();
            result.Data = await membershipBenefitRepository.GetAllAsync();
            return result;
        }
    }
}
