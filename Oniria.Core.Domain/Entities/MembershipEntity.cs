using System;

namespace Oniria.Core.Domain.Entities
{
    public class MembershipEntity : BaseEntity
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public int DurationDays { get; set; }
        public string MembershipCategoryId { get; set; }
    }
}