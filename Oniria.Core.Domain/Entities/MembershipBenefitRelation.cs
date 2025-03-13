namespace Oniria.Core.Domain.Entities
{
    public class MembershipBenefitRelation : BaseEntity
    {
        public bool Available { get; set; }
        public string MembershipId { get; set; }
        public string MembershipBenefitId { get; set; }
    }
}