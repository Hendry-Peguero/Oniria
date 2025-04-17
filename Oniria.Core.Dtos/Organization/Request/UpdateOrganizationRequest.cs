using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.Organization.Request
{
    public class UpdateOrganizationRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeOwnerId { get; set; }
        public StatusEntity Status { get; set; }
    }
}
