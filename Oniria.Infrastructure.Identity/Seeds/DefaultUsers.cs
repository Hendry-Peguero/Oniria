using Microsoft.AspNetCore.Identity;
using Oniria.Core.Domain.Enums;
using Oniria.Infrastructure.Identity.Entities;

namespace Oniria.Infrastructure.Identity.Seeds
{
    public class DefaultUsers
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
                Id = "1ok2l-vxztp-yub64-qm7fr-1298z",
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
            ApplicationUser doctor = new()
            {
                Id = "2ok2l-vxztp-yub64-qm7fr-1298z",
                UserName = "doctor",
                Email = "doctor@email.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(doctor.Email, userManager))
            {
                await userManager.CreateAsync(doctor, "123Pa$$Word!");
                await userManager.AddToRoleAsync(doctor, ActorsRoles.DOCTOR.ToString());
            }
        }

        public async static Task CreateAssistant(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser assistant = new()
            {
                Id = "3ok2l-vxztp-yub64-qm7fr-1298z",
                UserName = "assistant",
                Email = "assistant@email.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(assistant.Email, userManager))
            {
                await userManager.CreateAsync(assistant, "123Pa$$Word!");
                await userManager.AddToRoleAsync(assistant, ActorsRoles.ASSISTANT.ToString());
            }
        }

        public async static Task CreatePatients(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser patient = new()
            {
                Id = "4ok2l-vxztp-yub64-qm7fr-1298z",
                UserName = "patient",
                Email = "patient@email.com",
                Status = StatusEntity.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (!await EmailExists(patient.Email, userManager))
            {
                await userManager.CreateAsync(patient, "123Pa$$Word!");
                await userManager.AddToRoleAsync(patient, ActorsRoles.PATIENT.ToString());
            }
        }

        private async static Task<bool> EmailExists(string email, UserManager<ApplicationUser> userManager)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }
    }
}
