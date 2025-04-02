using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IEmotionalStatesRepository :
        GetAllAsync<EmotionalStatesEntity>,
        GetByIdAsync<EmotionalStatesEntity, string>
    {
    }
}
