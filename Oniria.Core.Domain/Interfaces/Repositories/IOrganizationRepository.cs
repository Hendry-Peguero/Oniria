using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
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
