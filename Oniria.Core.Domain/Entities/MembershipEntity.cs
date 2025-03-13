using System;

namespace Oniria.Core.Domain.Entities
{
    public class MembershipEntity : BaseEntity
    {
        public double Price { get; set; }
        public String Description { get; set; }
        public int DurationDays { get; set; }
        public String MembershipCategoryId { get; set; }
    }
}