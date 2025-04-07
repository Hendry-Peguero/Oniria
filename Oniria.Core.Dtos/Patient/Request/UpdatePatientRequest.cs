using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.Patient.Request
{
    public class UpdatePatientRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GenderId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public StatusEntity Status { get; set; }
    }
}
