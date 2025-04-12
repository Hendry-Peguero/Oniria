namespace Oniria.Core.Dtos.DreamToken.Request
{
    public class CreateDreamTokenRequest
    {
        public string PatientId { get; set; }
        public int? Amount { get; set; }
        public DateTime? RefreshDate { get; set; }
    }
}
