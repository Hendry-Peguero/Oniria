using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.MembershipCategory.Queries
{
    public class GetAllMembershipCategoryAsyncQuery : IRequest<OperationResult<List<MembershipCategoryEntity>>> { }

    public class GetAllMembershipCategoryAsyncQueryHandler : IRequestHandler<GetAllMembershipCategoryAsyncQuery, OperationResult<List<MembershipCategoryEntity>>>
    {
        private readonly IMembershipCategoryRepository membershipCategoryRepository;

        public GetAllMembershipCategoryAsyncQueryHandler(IMembershipCategoryRepository membershipCategoryRepository)
        {
            this.membershipCategoryRepository = membershipCategoryRepository;
        }

        public async Task<OperationResult<List<MembershipCategoryEntity>>> Handle(GetAllMembershipCategoryAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<MembershipCategoryEntity>>.Create();
            result.Data = await membershipCategoryRepository.GetAllAsync();
            return result;
        }
    }
}
