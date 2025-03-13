using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{


    public interface IOrganizationRepository :
    GetAllAsync<OrganizationEntity>,
    GetByIdAsync<OrganizationEntity, string>,
    CreateAsync<OrganizationEntity>,
    UpdateAsync<OrganizationEntity>,
    DeleteAsync<OrganizationEntity>
    {

    }
}
