using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{


    public interface IOrganizationRepository :
    GetAllAsync<GenderEntity>,
    GetByIdAsync<GenderEntity, string>,
    CreateAsync<GenderEntity>,
    UpdateAsync<GenderEntity>,
    DeleteAsync<GenderEntity>

    {

    }
}
