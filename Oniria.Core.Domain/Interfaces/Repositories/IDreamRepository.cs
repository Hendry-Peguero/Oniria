using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IDreamRepository :
    GetAllAsync<DreamEntity>,
    GetByIdAsync<DreamEntity, string>,
    CreateAsync<DreamEntity>,
    DeleteAsync<DreamEntity>
    {
    }
}
