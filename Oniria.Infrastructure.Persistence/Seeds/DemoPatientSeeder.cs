using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoPatientSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientEntity>().HasData(
                new PatientEntity
                {
                    Id = "p122l-vxztp-yub64-qm7fr-1298z",
                    Name = "Marcos",
                    LastName = "Pérez",
                    BornDate = new DateTime(1990, 6, 15),
                    PhoneNumber = "+1 809-555-9876",
                    Address = "Calle 10, Santo Domingo",
                    GenderId = "GM",
                    UserId = "p122l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = "org1-vxztp-yub64-qm7fr-1298z",
                    Status = StatusEntity.ACTIVE
                },
                new PatientEntity
                {
                    Id = "p222l-vxztp-yub64-qm7fr-1298z",
                    Name = "Miguela",
                    LastName = "Ramírez",
                    BornDate = new DateTime(1985, 3, 22),
                    PhoneNumber = "+1 829-555-4321",
                    Address = "Avenida Independencia, Santiago",
                    GenderId = "GF",
                    UserId = "p222l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = "org2-vxztp-yub64-qm7fr-1298z",
                    Status = StatusEntity.ACTIVE
                },
                new PatientEntity
                {
                    Id = "p322l-vxztp-yub64-qm7fr-1298z",
                    Name = "Ronald",
                    LastName = "Jiménez",
                    BornDate = new DateTime(1992, 11, 5),
                    PhoneNumber = "+1 809-555-6543",
                    Address = "Calle Principal, Santo Domingo",
                    GenderId = "GM",
                    UserId = "p322l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = "org2-vxztp-yub64-qm7fr-1298z",
                    Status = StatusEntity.ACTIVE
                }
            );
        }
    }
}
