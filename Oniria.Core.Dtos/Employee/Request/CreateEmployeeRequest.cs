namespace Oniria.Core.Dtos.Employee.Request
{
    public class CreateEmployeeRequest
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public string? OrganizationId { get; set; }
    }
}
