using System.ComponentModel.DataAnnotations;

namespace Oniria.Core.Dtos.Employee.Request
{
    public class CreateEmployeeByOrganizationRequest
    {
        public string Dni { get; set; }
        public string Email { get; set; }      
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? OrganizationId { get; set; }
    }
}
