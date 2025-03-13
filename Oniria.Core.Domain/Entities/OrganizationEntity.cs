namespace Oniria.Core.Domain.Entities
{
    public class OrganizationEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeOwnerld { get; set; }
    }
}
