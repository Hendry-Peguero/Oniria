namespace Oniria.Core.Domain.Entities
{
    public class DreamTokenEntity : BaseEntity
    {
        public string PatientId { get; set; }
        public int? Amount { get; set; }
        public DateTime? RefreshDate { get; set; }
    }
}
