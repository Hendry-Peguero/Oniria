using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultMembershipBenefitSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipBenefitEntity>().HasData(
                new MembershipBenefitEntity { Id = "PB1", Description = "Limited dream recording & analysis", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB2", Description = "General recommendations", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB3", Description = "Summarized history", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB4", Description = "Essential features", Status = StatusEntity.ACTIVE },

                new MembershipBenefitEntity { Id = "PS1", Description = "Increased dream recording", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS2", Description = "Personalized analysis & suggestions", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS3", Description = "Evolution statistics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS4", Description = "Enhanced profile & support", Status = StatusEntity.ACTIVE },

                new MembershipBenefitEntity { Id = "PP1", Description = "Unlimited dream recording", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP2", Description = "Advanced, real-time analysis", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP3", Description = "Wearable integration", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP4", Description = "Priority support & exclusive updates", Status = StatusEntity.ACTIVE },

                new MembershipBenefitEntity { Id = "OB1", Description = "Manage up to 20 users", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OB2", Description = "Basic dashboard metrics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OB3", Description = "Essential administration features", Status = StatusEntity.ACTIVE },

                new MembershipBenefitEntity { Id = "OS1", Description = "Manage up to 100 users", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS2", Description = "Advanced dashboard with detailed reports", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS3", Description = "Basic system integration", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS4", Description = "Dedicated support", Status = StatusEntity.ACTIVE },

                new MembershipBenefitEntity { Id = "OP1", Description = "Unlimited user management", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP2", Description = "Customizable reports & analytics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP3", Description = "Full integration with internal systems", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP4", Description = "Priority support & consultancy", Status = StatusEntity.ACTIVE }
            );
        }
    }
}
