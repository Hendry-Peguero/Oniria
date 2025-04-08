using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Interfaces.Repositories.Maintenance;

namespace Oniria.Core.Domain.Interfaces.Repositories
{
    public interface IMembershipBenefitRelationRepository :
    GetAllAsync<MembershipBenefitRelationEntity>,
    GetByIdAsync<MembershipBenefitRelationEntity, string>,
    UpdateAsync<MembershipBenefitRelationEntity>,
    DeleteAsync<MembershipBenefitRelationEntity>,
    CreateAsync<MembershipBenefitRelationEntity>
    {
    }
}
