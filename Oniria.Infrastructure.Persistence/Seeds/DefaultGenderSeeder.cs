using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultGenderSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderEntity>().HasData(
                new GenderEntity { Id = "GM", Description = "Male", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = "GF", Description = "Female", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = "GPNS", Description = "Prefer not to say", Status = StatusEntity.ACTIVE }
            );
        }
    }
}
