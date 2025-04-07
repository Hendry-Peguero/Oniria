namespace Oniria.Core.Dtos.Patient.Request
{
    public class CreatePatientRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string GenderId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }
}
