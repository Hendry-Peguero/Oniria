using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoEmployeeSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().HasData(
                new EmployeeEntity
                {
                    Id = "emp1l-vxztp-yub64-qm7fr-1298z",
                    Dni = "00112345678",
                    Name = "Laura",
                    LastName = "Martínez",
                    BornDate = new DateTime(1985, 4, 23),
                    PhoneNumber = "+1 809-555-1234",
                    Address = "Calle Duarte #45, Santo Domingo",
                    UserId = "d1k2l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    Status = StatusEntity.ACTIVE
                },
                new EmployeeEntity
                {
                    Id = "emp2l-vxztp-yub64-qm7fr-1298z",
                    Dni = "00276543213",
                    Name = "Carlos",
                    LastName = "Gómez",
                    BornDate = new DateTime(1990, 10, 5),
                    PhoneNumber = "+1 829-321-5678",
                    Address = "Av. 27 de Febrero, Santiago",
                    UserId = "d2k2l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    Status = StatusEntity.ACTIVE
                },
                new EmployeeEntity
                {
                    Id = "emp3l-vxztp-yub64-qm7fr-1298z",
                    Dni = "00345678901",
                    Name = "Elena",
                    LastName = "Rodríguez",
                    BornDate = new DateTime(1992, 6, 17),
                    PhoneNumber = "+1 849-678-9012",
                    Address = "Calle Las Palmas #12, San Cristóbal",
                    UserId = "as12l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    Status = StatusEntity.ACTIVE
                },
                new EmployeeEntity
                {
                    Id = "emp4l-vxztp-yub64-qm7fr-1298z",
                    Dni = "00498765439",
                    Name = "Miguel",
                    LastName = "Fernández",
                    BornDate = new DateTime(1988, 2, 11),
                    PhoneNumber = "+1 809-999-2233",
                    Address = "Av. Independencia, La Vega",
                    UserId = "as22l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    Status = StatusEntity.ACTIVE
                },
                new EmployeeEntity
                {
                    Id = "emp5l-vxztp-yub64-qm7fr-1298z",
                    Dni = "00532109876",
                    Name = "Camila",
                    LastName = "Santos",
                    BornDate = new DateTime(1995, 12, 30),
                    PhoneNumber = "+1 829-888-4455",
                    Address = "Calle del Sol, Puerto Plata",
                    UserId = "d3k2l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    Status = StatusEntity.ACTIVE
                }
            );
        }
    }
}
