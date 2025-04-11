namespace Oniria.Core.Domain.Entities
{
    public class MembershipAcquisitionEntity : BaseEntity
    {
        public string OwnerId { get; set; }
        public string MembershipId { get; set; }
        public DateTime AcquisitionDate { get; set; }

        //nav
        public PatientEntity Owner { get; set; }
        public MembershipEntity Membership { get; set; }
    }
}
