namespace Oniria.Core.Domain.Entities
{
    public class MembershipBenefitRelationEntity : BaseEntity
    {
        public bool Available { get; set; }
        public string MembershipId { get; set; }
        public string MembershipBenefitId { get; set; }
    }
}