namespace Oniria.Core.Dtos.Organization.Request
{
    public class RegisterOrganizationRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeDni { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime EmployeeBornDate { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeeAddress { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationPhoneNumber { get; set; }
        public string MembershipId { get; set; }
    }
}
