using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipBenefitRelation.Queries
{
    public class GetMembershipBenefitRelationByIdAsyncQuery : IRequest<OperationResult<MembershipBenefitRelationEntity>>
    {
        public string Id { get; set; }
    }

    public class GetMembershipBenefitRelationByIdAsyncQueryHandler : IRequestHandler<GetMembershipBenefitRelationByIdAsyncQuery, OperationResult<MembershipBenefitRelationEntity>>
    {
        private readonly IMembershipBenefitRelationRepository membershipBenefitRelationRepository;

        public GetMembershipBenefitRelationByIdAsyncQueryHandler(IMembershipBenefitRelationRepository membershipBenefitRelationRepository)
        {
            this.membershipBenefitRelationRepository = membershipBenefitRelationRepository;
        }

        public async Task<OperationResult<MembershipBenefitRelationEntity>> Handle(GetMembershipBenefitRelationByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipBenefitRelationEntity>.Create();
            var entity = await membershipBenefitRelationRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddError("Could not obtain the membership benefit relation by ID");
            }
            else
            {
                result.Data = entity;
            }

            return result;
        }
    }
}
