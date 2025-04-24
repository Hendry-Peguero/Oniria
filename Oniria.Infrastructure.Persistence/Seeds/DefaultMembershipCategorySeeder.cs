using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultMembershipCategorySeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipCategoryEntity>().HasData(
                new MembershipCategoryEntity { Id = "P", Description = MembershipCategoryTypes.Patient.ToString(), Status = StatusEntity.ACTIVE },
                new MembershipCategoryEntity { Id = "O", Description = MembershipCategoryTypes.Organization.ToString(), Status = StatusEntity.ACTIVE }
            );
        }
    }
}
