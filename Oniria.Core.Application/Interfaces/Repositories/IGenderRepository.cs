using Oniria.Core.Application.Interfaces.Repositories.Maintenance;
using Oniria.Core.Domain.Entities;

namespace Oniria.Core.Application.Interfaces.Repositories
{
    public interface IGenderRepository :
    GetAllAsync<GenderEntity>,
    GetByIdAsync<GenderEntity, string>
    { 
    
    }
}
