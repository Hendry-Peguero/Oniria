using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IMembershipAcquisitionRepository :
    GetAllAsync<MembershipAcquisitionEntity>,
    GetByIdAsync<MembershipAcquisitionEntity, string>,
    UpdateAsync<MembershipAcquisitionEntity>,
    DeleteAsync<MembershipAcquisitionEntity>,
    CreateAsync<MembershipAcquisitionEntity>
    {

    }
}
