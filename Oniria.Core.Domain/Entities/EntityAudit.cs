using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Domain.Entities
{
    public class EntityAudit
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public StatusEntity Status { get; set; }
    }
}
