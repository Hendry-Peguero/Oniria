namespace Oniria.Core.Dtos.Organization.Request
{
    public class CreateOrganizationRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeOwnerId { get; set; }
    }
}
