namespace Oniria.Core.Domain.Entities
{
    public class MembershipCategoryEntity : BaseEntity
    {
        public string Description { get; set; }

        // nav
        public ICollection<MembershipEntity> Memberships { get; set; }
    }
}
