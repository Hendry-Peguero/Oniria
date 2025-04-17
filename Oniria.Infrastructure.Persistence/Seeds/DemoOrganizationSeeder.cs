using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoOrganizationSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationEntity>().HasData(
                new OrganizationEntity
                {
                    Id = "org1-vxztp-yub64-qm7fr-1298z",
                    Name = "Consultorio Psicólogico Laura",
                    Address = "Calle Duarte #45, Santo Domingo",
                    PhoneNumber = "+1 809-555-1234",
                    EmployeeOwnerId = "emp1l-vxztp-yub64-qm7fr-1298z",
                    Status = StatusEntity.ACTIVE
                },
                new OrganizationEntity
                {
                    Id = "org2-vxztp-yub64-qm7fr-1298z",
                    Name = "Consultorio Psicólogico Carlos",
                    Address = "Av. 27 de Febrero, Santiago",
                    PhoneNumber = "+1 829-321-5678",
                    EmployeeOwnerId = "emp2l-vxztp-yub64-qm7fr-1298z",
                    Status = StatusEntity.ACTIVE
                }
            );
        }
    }
}
