using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IDreamAnalysisRepository :
    GetAllAsync<DreamAnalysisEntity>,
    GetByIdAsync<DreamAnalysisEntity, string>,
    CreateAsync<DreamAnalysisEntity>
    {


    }
}
