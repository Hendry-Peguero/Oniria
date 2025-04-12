using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipBenefitRelation.Queries
{
    public class GetAllMembershipBenefitRelationAsyncQuery : IRequest<OperationResult<List<MembershipBenefitRelationEntity>>> { }

    public class GetAllMembershipBenefitRelationAsyncQueryHandler : IRequestHandler<GetAllMembershipBenefitRelationAsyncQuery, OperationResult<List<MembershipBenefitRelationEntity>>>
    {
        private readonly IMembershipBenefitRelationRepository membershipBenefitRelationRepository;

        public GetAllMembershipBenefitRelationAsyncQueryHandler(IMembershipBenefitRelationRepository membershipBenefitRelationRepository)
        {
            this.membershipBenefitRelationRepository = membershipBenefitRelationRepository;
        }

        public async Task<OperationResult<List<MembershipBenefitRelationEntity>>> Handle(GetAllMembershipBenefitRelationAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<MembershipBenefitRelationEntity>>.Create();
            result.Data = await membershipBenefitRelationRepository.GetAllAsync();
            return result;
        }
    }
}
