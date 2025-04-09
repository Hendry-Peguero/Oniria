using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipAcquisition.Queries
{
    public class GetMembershipAcquisitionByIdAsyncQuery : IRequest<OperationResult<MembershipAcquisitionEntity>>
    {
        public string Id { get; set; }
    }

    public class GetMembershipAcquisitionByIdAsyncQueryHandler : IRequestHandler<GetMembershipAcquisitionByIdAsyncQuery, OperationResult<MembershipAcquisitionEntity>>
    {
        private readonly IMembershipAcquisitionRepository membershipAcquisitionRepository;

        public GetMembershipAcquisitionByIdAsyncQueryHandler(IMembershipAcquisitionRepository membershipAcquisitionRepository)
        {
            this.membershipAcquisitionRepository = membershipAcquisitionRepository;
        }

        public async Task<OperationResult<MembershipAcquisitionEntity>> Handle(GetMembershipAcquisitionByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipAcquisitionEntity>.Create();
            var entity = await membershipAcquisitionRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                result.AddError("Could not obtain the membership acquisition by ID");
            }
            else
            {
                result.Data = entity;
            }

            return result;
        }
    }
}
