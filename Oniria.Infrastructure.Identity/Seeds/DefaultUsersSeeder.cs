using Microsoft.AspNetCore.Identity;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Enums;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Seeds
{
    public class DefaultUsersSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            await CreateAdmins(userManager);
            await CreateDoctors(userManager);
            await CreateAssistant(userManager);
            await CreatePatients(userManager);
        }

        public async static Task CreateAdmins(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser admin = new()
            {
                Id = "a1k2l-vxztp-yub64-qm7fr-1298z",
                UserName = "admin",
                Email = "admin@email.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(admin.Email, userManager))
            {
                await userManager.CreateAsync(admin, "123Pa$$Word!");
                await userManager.AddToRoleAsync(admin, ActorsRoles.ADMIN.ToString());
            }
        }

        public async static Task CreateDoctors(UserManager<ApplicationUser> userManager)
        {
            // Laura
            ApplicationUser laura = new()
            {
                Id = "d1k2l-vxztp-yub64-qm7fr-1298z",
                UserName = "laura",
                Email = "laura@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(laura.Email, userManager))
            {
                await userManager.CreateAsync(laura, "123Pa$$Word!");
                await userManager.AddToRoleAsync(laura, ActorsRoles.DOCTOR.ToString());
            }

            // Carlos
            ApplicationUser carlos = new()
            {
                Id = "d2k2l-vxztp-yub64-qm7fr-1298z",
                UserName = "carlos",
                Email = "carlos@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(carlos.Email, userManager))
            {
                await userManager.CreateAsync(carlos, "123Pa$$Word!");
                await userManager.AddToRoleAsync(carlos, ActorsRoles.DOCTOR.ToString());
            }

            // Camila
            ApplicationUser camila = new()
            {
                Id = "d3k2l-vxztp-yub64-qm7fr-1298z",
                UserName = "camila",
                Email = "camila@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(camila.Email, userManager))
            {
                await userManager.CreateAsync(camila, "123Pa$$Word!");
                await userManager.AddToRoleAsync(camila, ActorsRoles.DOCTOR.ToString());
            }
        }

        public async static Task CreateAssistant(UserManager<ApplicationUser> userManager)
        {
            // Elena
            ApplicationUser elena = new()
            {
                Id = "as12l-vxztp-yub64-qm7fr-1298z",
                UserName = "elena",
                Email = "elena@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(elena.Email, userManager))
            {
                await userManager.CreateAsync(elena, "123Pa$$Word!");
                await userManager.AddToRoleAsync(elena, ActorsRoles.ASSISTANT.ToString());
            }

            // Miguel
            ApplicationUser miguel = new()
            {
                Id = "as22l-vxztp-yub64-qm7fr-1298z",
                UserName = "miguel",
                Email = "miguel@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(miguel.Email, userManager))
            {
                await userManager.CreateAsync(miguel, "123Pa$$Word!");
                await userManager.AddToRoleAsync(miguel, ActorsRoles.ASSISTANT.ToString());
            }
        }

        public async static Task CreatePatients(UserManager<ApplicationUser> userManager)
        {
            // Marcos
            ApplicationUser marcos = new()
            {
                Id = "p122l-vxztp-yub64-qm7fr-1298z",
                UserName = "marcos",
                Email = "marcos@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(marcos.Email, userManager))
            {
                await userManager.CreateAsync(marcos, "123Pa$$Word!");
                await userManager.AddToRoleAsync(marcos, ActorsRoles.PATIENT.ToString());
            }

            // Miguela
            ApplicationUser miguela = new()
            {
                Id = "p222l-vxztp-yub64-qm7fr-1298z",
                UserName = "miguela",
                Email = "miguela@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(miguela.Email, userManager))
            {
                await userManager.CreateAsync(miguela, "123Pa$$Word!");
                await userManager.AddToRoleAsync(miguela, ActorsRoles.PATIENT.ToString());
            }

            // Ronald
            ApplicationUser ronald = new()
            {
                Id = "p322l-vxztp-yub64-qm7fr-1298z",
                UserName = "ronald",
                Email = "ronald@gmail.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(ronald.Email, userManager))
            {
                await userManager.CreateAsync(ronald, "123Pa$$Word!");
                await userManager.AddToRoleAsync(ronald, ActorsRoles.PATIENT.ToString());
            }
        }

        private async static Task<bool> EmailExists(string email, UserManager<ApplicationUser> userManager)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }
    }
}
