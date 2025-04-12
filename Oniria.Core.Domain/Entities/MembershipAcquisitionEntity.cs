namespace Oniria.Core.Domain.Entities
{
    public class MembershipAcquisitionEntity : BaseEntity
    {
        public string? OrganizationId { get; set; }
        public string? PatientId { get; set; }
        public string MembershipId { get; set; }
        public DateTime AcquisitionDate { get; set; }

        //nav
        public OrganizationEntity? Organization { get; set; }
        public PatientEntity? Patient { get; set; }
        public MembershipEntity Membership { get; set; }
    }
}
