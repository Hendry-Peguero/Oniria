using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultMembershipSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipEntity>().HasData(
                // Patient Memberships
                new MembershipEntity
                {
                    Id = "PB",
                    Price = 9.99,
                    Description = "Basic",
                    DurationDays = 30,
                    MembershipCategoryId = "P"
                },
                new MembershipEntity
                {
                    Id = "PS",
                    Price = 19.99,
                    Description = "Standard",
                    DurationDays = 90,
                    MembershipCategoryId = "P"
                },
                new MembershipEntity
                {
                    Id = "PP",
                    Price = 29.99,
                    Description = "Premium",
                    DurationDays = 365,
                    MembershipCategoryId = "P"
                },

                // Organization Memberships
                new MembershipEntity
                {
                    Id = "OB",
                    Price = 49.99,
                    Description = "Basic",
                    DurationDays = 30,
                    MembershipCategoryId = "O"
                },
                new MembershipEntity
                {
                    Id = "OS",
                    Price = 99.99,
                    Description = "Standard",
                    DurationDays = 90,
                    MembershipCategoryId = "O"
                },
                new MembershipEntity
                {
                    Id = "OP",
                    Price = 199.99,
                    Description = "Premium",
                    DurationDays = 365,
                    MembershipCategoryId = "O"
                }
            );
        }
    }
}
