namespace Oniria.Core.Dtos.Membership.Request
{
    public class CreateMembershipRequest
    {
        public string MembershipCategoryId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }
    }
}
