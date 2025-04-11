using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Membership.Queries
{
    public class GetMembershipByIdAsyncQuery : IRequest<OperationResult<MembershipEntity>>
    {
        public string Id { get; set; }
    }

    public class GetMembershipByIdAsyncQueryHandler : IRequestHandler<GetMembershipByIdAsyncQuery, OperationResult<MembershipEntity>>
    {
        private readonly IMembershipRepository membershipRepository;

        public GetMembershipByIdAsyncQueryHandler(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public async Task<OperationResult<MembershipEntity>> Handle(GetMembershipByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipEntity>.Create();
            var membership = await membershipRepository.GetByIdAsync(request.Id);

            if (membership == null)
            {
                result.AddError("Could not get membership by ID");
            }
            else
            {
                result.Data = membership;
            }

            return result;
        }
    }
}
