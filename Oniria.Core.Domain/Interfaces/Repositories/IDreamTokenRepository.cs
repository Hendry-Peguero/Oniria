using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IDreamTokenRepository :
    GetAllAsync<DreamTokenEntity>,
    GetByIdAsync<DreamTokenEntity, string>,
    CreateAsync<DreamTokenEntity>,
    UpdateAsync<DreamTokenEntity>
    {
    }
}
