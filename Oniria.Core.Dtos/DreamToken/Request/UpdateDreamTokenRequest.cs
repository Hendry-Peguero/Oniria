using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.DreamToken.Request
{
    public class UpdateDreamTokenRequest
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public int? Amount { get; set; }
        public DateTime? RefreshDate { get; set; }
        public StatusEntity Status { get; set; }
    }
}
