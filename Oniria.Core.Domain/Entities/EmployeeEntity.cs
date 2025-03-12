namespace Oniria.Core.Domain.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public String Dni {  get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public DateTime BornDate { get; set; }
        public String PhoneNumber { get; set; }
        public String Address { get; set; }
        public String UserId { get; set; }
        public String OrganizationId { get; set; }
    }
}
