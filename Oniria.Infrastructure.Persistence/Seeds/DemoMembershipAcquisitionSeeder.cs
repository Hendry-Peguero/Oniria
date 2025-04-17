using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoMembershipAcquisitionSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipAcquisitionEntity>().HasData(
                // Adquisiciones de Membresía de Pacientes
                new MembershipAcquisitionEntity
                {
                    Id = "acq1-p122l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    PatientId = "p122l-vxztp-yub64-qm7fr-1298z",
                    MembershipId = "PB",
                    AcquisitionDate = new DateTime(2025, 4, 10)
                },
                new MembershipAcquisitionEntity
                {
                    Id = "acq2-p222l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    PatientId = "p222l-vxztp-yub64-qm7fr-1298z",
                    MembershipId = "PS",
                    AcquisitionDate = new DateTime(2025, 4, 10)
                },
                new MembershipAcquisitionEntity
                {
                    Id = "acq3-p322l-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = null,
                    PatientId = "p322l-vxztp-yub64-qm7fr-1298z",
                    MembershipId = "PP",
                    AcquisitionDate = new DateTime(2025, 4, 10)
                },

                // Adquisiciones de Membresía de Organizaciones
                new MembershipAcquisitionEntity
                {
                    Id = "acq4-org1-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = "org1-vxztp-yub64-qm7fr-1298z",
                    PatientId = null,
                    MembershipId = "OB",
                    AcquisitionDate = new DateTime(2025, 4, 10)
                },
                new MembershipAcquisitionEntity
                {
                    Id = "acq5-org2-vxztp-yub64-qm7fr-1298z",
                    OrganizationId = "org2-vxztp-yub64-qm7fr-1298z",
                    PatientId = null,
                    MembershipId = "OS",
                    AcquisitionDate = new DateTime(2025, 4, 10)
                }
            );
        }
    }
}
