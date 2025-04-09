using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Organization.Queries
{
    public class GetAllOrganizationAsyncQuery : IRequest<OperationResult<List<OrganizationEntity>>> { }

    public class GetAllOrganizationAsyncQueryHandler : IRequestHandler<GetAllOrganizationAsyncQuery, OperationResult<List<OrganizationEntity>>>
    {
        private readonly IOrganizationRepository organizationRepository;

        public GetAllOrganizationAsyncQueryHandler(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<OperationResult<List<OrganizationEntity>>> Handle(GetAllOrganizationAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<List<OrganizationEntity>>.Create();

            result.Data = await organizationRepository.GetAllAsync();

            return result;
        }
    }
}
