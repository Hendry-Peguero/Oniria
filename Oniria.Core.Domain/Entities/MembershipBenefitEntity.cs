namespace Oniria.Core.Domain.Entities
{
    public class MembershipBenefitEntity : BaseEntity
    {
        public string Description { get; set; }

        // nav
        public ICollection<MembershipBenefitRelationEntity> BenefitRelations { get; set; }
    }
}
