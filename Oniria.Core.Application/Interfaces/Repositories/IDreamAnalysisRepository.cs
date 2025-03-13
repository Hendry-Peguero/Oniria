using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IDreamAnalysisRepository :
    GetAllAsync<DreamAnalysisEntity>,
    GetByIdAsync<DreamAnalysisEntity, string>,
    CreateAsync<DreamAnalysisEntity>
    {


    }
}
