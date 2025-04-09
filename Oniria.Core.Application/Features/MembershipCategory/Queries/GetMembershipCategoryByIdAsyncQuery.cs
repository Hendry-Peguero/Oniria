using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipCategory.Queries
{
    public class GetMembershipCategoryByIdAsyncQuery : IRequest<OperationResult<MembershipCategoryEntity>>
    {
        public string Id { get; set; }
    }

    public class GetMembershipCategoryByIdAsyncQueryHandler : IRequestHandler<GetMembershipCategoryByIdAsyncQuery, OperationResult<MembershipCategoryEntity>>
    {
        private readonly IMembershipCategoryRepository membershipCategoryRepository;

        public GetMembershipCategoryByIdAsyncQueryHandler(IMembershipCategoryRepository membershipCategoryRepository)
        {
            this.membershipCategoryRepository = membershipCategoryRepository;
        }

        public async Task<OperationResult<MembershipCategoryEntity>> Handle(GetMembershipCategoryByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<MembershipCategoryEntity>.Create();
            var membershipCategory = await membershipCategoryRepository.GetByIdAsync(request.Id);

            if (membershipCategory == null)
            {
                result.AddError("Could not obtain the membership category by ID");
            }
            else
            {
                result.Data = membershipCategory;
            }

            return result;
        }
    }
}
