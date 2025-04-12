namespace Oniria.Core.Domain.Entities
{
    public class GenderEntity : BaseEntity
    {
        public string Description { get; set; }

        // nav
        public ICollection<PatientEntity> Patients { get; set; }
    }
}
