using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories;

namespace Oniria.Core.Application.Features.Organization.Queries
{
    public class GetOrganizationByIdAsyncQuery : IRequest<OperationResult<OrganizationEntity>>
    {
        public string Id { get; set; }
    }

    public class GetOrganizationByIdAsyncQueryHandler : IRequestHandler<GetOrganizationByIdAsyncQuery, OperationResult<OrganizationEntity>>
    {
        private readonly IOrganizationRepository organizationRepository;

        public GetOrganizationByIdAsyncQueryHandler(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<OperationResult<OrganizationEntity>> Handle(GetOrganizationByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = OperationResult<OrganizationEntity>.Create();
            var organization = await organizationRepository.GetByIdAsync(request.Id);

            if (organization == null)
            {
                result.AddError("Could not obtain the organization by ID");
            }
            else
            {
                result.Data = organization;
            }

            return result;
        }
    }
}
