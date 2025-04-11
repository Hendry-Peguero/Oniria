namespace Oniria.Core.Domain.Entities
{
    public class PatientEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GenderId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }

        //nav
        public OrganizationEntity Organization { get; set; }
        public GenderEntity Gender { get; set; }
        public ICollection<MembershipAcquisitionEntity> MembershipAcquisitions { get; set; }
        public ICollection<DreamEntity> Dreams { get; set; }
        public DreamTokenEntity DreamToken { get; set; }
    }
}
