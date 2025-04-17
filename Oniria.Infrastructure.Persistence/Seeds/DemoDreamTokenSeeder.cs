using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoDreamTokenSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamTokenEntity>().HasData(
                new DreamTokenEntity
                {
                    Id = "dtok1-p122l-vxztp-yub64-qm7fr-1298z",
                    PatientId = "p122l-vxztp-yub64-qm7fr-1298z",
                    Amount = 5,
                    RefreshDate = null
                },
                new DreamTokenEntity
                {
                    Id = "dtok2-p222l-vxztp-yub64-qm7fr-1298z",
                    PatientId = "p222l-vxztp-yub64-qm7fr-1298z",
                    Amount = 3,
                    RefreshDate = null
                },
                new DreamTokenEntity
                {
                    Id = "dtok3-p322l-vxztp-yub64-qm7fr-1298z",
                    PatientId = "p322l-vxztp-yub64-qm7fr-1298z",
                    Amount = 7,
                    RefreshDate = null
                }
            );
        }
    }
}
