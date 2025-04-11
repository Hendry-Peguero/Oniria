using Oniria.Core.Domain.Enums;

namespace Oniria.Core.Dtos.Membership.Request
{
    public class UpdateMembershipRequest
    {
        public int Id { get; set; }
        public string MembershipCategoryId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }
        public StatusEntity Status { get; set; }
    }
}
