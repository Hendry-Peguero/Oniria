namespace Oniria.Core.Domain.Entities
{
    public class OrganizationEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeOwnerId { get; set; }

        //nav 
        public EmployeeEntity EmployeeOwner { get; set; }
        public ICollection<EmployeeEntity> Employees { get; set; }
        public ICollection<PatientEntity> Patients { get; set; }
        public ICollection<MembershipAcquisitionEntity> MembershipAcquisitions { get; set; }
    }
}
