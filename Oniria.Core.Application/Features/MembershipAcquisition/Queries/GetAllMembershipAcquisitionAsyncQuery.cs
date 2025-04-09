using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipAcquisition.Queries
{
    public class GetAllMembershipAcquisitionAsyncQuery : IRequest<OperationResult<List<MembershipAcquisitionEntity>>> { }

    public class GetAllMembershipAcquisitionAsyncQueryHandler : IRequestHandler<GetAllMembershipAcquisitionAsyncQuery, OperationResult<List<MembershipAcquisitionEntity>>>
    {
        private readonly IMembershipAcquisitionRepository membershipAcquisitionRepository;

        public GetAllMembershipAcquisitionAsyncQueryHandler(IMembershipAcquisitionRepository membershipAcquisitionRepository)
        {
            this.membershipAcquisitionRepository = membershipAcquisitionRepository;
        }

        public async Task<OperationResult<List<MembershipAcquisitionEntity>>> Handle(GetAllMembershipAcquisitionAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<MembershipAcquisitionEntity>>.Create();
            result.Data = await membershipAcquisitionRepository.GetAllAsync();
            return result;
        }
    }
}
