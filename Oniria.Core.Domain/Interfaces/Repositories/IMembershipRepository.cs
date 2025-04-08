using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IMembershipRepository :
     GetAllAsync<MembershipEntity>,
     GetByIdAsync<MembershipEntity, string>
    {
    }
}
