using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IDreamTokenRepository :
    GetAllAsync<DreamTokenEntity>,
    GetByIdAsync<DreamTokenEntity, string>,
    CreateAsync<DreamTokenEntity>,
    UpdateAsync<DreamTokenEntity>
    {
    }
}
