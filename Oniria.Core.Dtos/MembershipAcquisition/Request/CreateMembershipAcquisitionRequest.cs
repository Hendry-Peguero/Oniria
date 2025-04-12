namespace Oniria.Core.Dtos.MembershipAcquisition.Request
{
    public class CreateMembershipAcquisitionRequest
    {
        public string? PatientId { get; set; }
        public string? OrganizationId { get; set; }
        public string MembershipId { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
