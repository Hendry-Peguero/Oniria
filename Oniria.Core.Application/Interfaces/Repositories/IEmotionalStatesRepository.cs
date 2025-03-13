using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IEmotionalStatesRepository :
        GetAllAsync<EmotionalStatesEntity>,
        GetByIdAsync<EmotionalStatesEntity, string>
    {
    }
}
