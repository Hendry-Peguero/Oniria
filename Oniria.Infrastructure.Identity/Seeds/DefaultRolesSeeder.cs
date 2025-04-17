using Microsoft.AspNetCore.Identity;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Identity.Seeds
{
    public static class DefaultRolesSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ActorsRoles.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ActorsRoles.DOCTOR.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ActorsRoles.ASSISTANT.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ActorsRoles.PATIENT.ToString()));
        }
    }
}
