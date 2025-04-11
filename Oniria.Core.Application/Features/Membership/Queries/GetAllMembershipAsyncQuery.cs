using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Membership.Queries
{
    public class GetAllMembershipAsyncQuery : IRequest<OperationResult<List<MembershipEntity>>> { }

    public class GetAllMembershipAsyncQueryHandler : IRequestHandler<GetAllMembershipAsyncQuery, OperationResult<List<MembershipEntity>>>
    {
        private readonly IMembershipRepository membershipRepository;

        public GetAllMembershipAsyncQueryHandler(IMembershipRepository membershipRepository )
        {
            this.membershipRepository = membershipRepository;
        }

        public async Task<OperationResult<List<MembershipEntity>>> Handle(GetAllMembershipAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<MembershipEntity>>.Create();

            result.Data = await membershipRepository.GetAllAsync();

            return result;
        }
    }
}
