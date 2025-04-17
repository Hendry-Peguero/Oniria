using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oniria.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedFix_EmployeeOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Employees SET OrganizationId = 'org1-vxztp-yub64-qm7fr-1298z' WHERE Id = 'emp1l-vxztp-yub64-qm7fr-1298z';
                UPDATE Employees SET OrganizationId = 'org2-vxztp-yub64-qm7fr-1298z' WHERE Id = 'emp2l-vxztp-yub64-qm7fr-1298z';
                UPDATE Employees SET OrganizationId = 'org1-vxztp-yub64-qm7fr-1298z' WHERE Id = 'emp3l-vxztp-yub64-qm7fr-1298z';
                UPDATE Employees SET OrganizationId = 'org2-vxztp-yub64-qm7fr-1298z' WHERE Id = 'emp4l-vxztp-yub64-qm7fr-1298z';
                UPDATE Employees SET OrganizationId = 'org2-vxztp-yub64-qm7fr-1298z' WHERE Id = 'emp5l-vxztp-yub64-qm7fr-1298z';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Employees SET OrganizationId = NULL WHERE Id IN (
                    'emp1l-vxztp-yub64-qm7fr-1298z',
                    'emp2l-vxztp-yub64-qm7fr-1298z',
                    'emp3l-vxztp-yub64-qm7fr-1298z',
                    'emp4l-vxztp-yub64-qm7fr-1298z',
                    'emp5l-vxztp-yub64-qm7fr-1298z'
                );
            ");
        }
    }
}
