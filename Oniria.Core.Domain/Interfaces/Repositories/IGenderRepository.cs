using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IGenderRepository :
    GetAllAsync<GenderEntity>,
    GetByIdAsync<GenderEntity, string>
    {

    }
}
