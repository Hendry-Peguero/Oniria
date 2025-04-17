using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoDreamSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamEntity>().HasData(
                new DreamEntity
                {
                    Id = "drm1-p122l-vxztp-yub64-qm7fr-1298z",
                    Title = "Caída Infinita",
                    Prompt = "Soñé que caía por un pozo sin fondo, no podía gritar ni moverme.",
                    PatientId = "p122l-vxztp-yub64-qm7fr-1298z"
                },

                new DreamEntity
                {
                    Id = "drm2-p222l-vxztp-yub64-qm7fr-1298z",
                    Title = "Casa en Llamas",
                    Prompt = "Estaba en mi casa y comenzaba a incendiarse, trataba de salvar a mis mascotas.",
                    PatientId = "p222l-vxztp-yub64-qm7fr-1298z"
                },
                new DreamEntity
                {
                    Id = "drm3-p222l-vxztp-yub64-qm7fr-1298z",
                    Title = "Mar Revuelto",
                    Prompt = "Estaba en una playa y una gran ola venía hacia mí. No podía correr.",
                    PatientId = "p222l-vxztp-yub64-qm7fr-1298z"
                },

                new DreamEntity
                {
                    Id = "drm4-p322l-vxztp-yub64-qm7fr-1298z",
                    Title = "Persecución en la Ciudad",
                    Prompt = "Un desconocido me perseguía por una ciudad oscura y no encontraba salida.",
                    PatientId = "p322l-vxztp-yub64-qm7fr-1298z"
                },
                new DreamEntity
                {
                    Id = "drm5-p322l-vxztp-yub64-qm7fr-1298z",
                    Title = "Vuelo sin Control",
                    Prompt = "Podía volar, pero no sabía cómo aterrizar y cada vez subía más alto.",
                    PatientId = "p322l-vxztp-yub64-qm7fr-1298z"
                }
            );
        }
    }
}
