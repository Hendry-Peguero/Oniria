using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IDreamRepository :
    GetAllAsync<DreamEntity>,
    GetByIdAsync<DreamEntity, string>
    {
        Task<DreamEntity> CreateAsync(DreamEntity dream);
    }
}
