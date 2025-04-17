using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DemoDreamAnalysisSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DreamAnalysisEntity>().HasData(
                new DreamAnalysisEntity
                {
                    Id = "dana1-drm1",
                    Title = "Análisis: Caída Infinita",
                    DreamId = "drm1-p122l-vxztp-yub64-qm7fr-1298z",
                    EmotionalStateId = "ems4-p222l-vxztp-yub64-qm7fr-1298z",
                    Recommendation = "Practicar técnicas de grounding para manejar sensaciones de pérdida de control.",
                    PatternBehaviour = "Sensación de falta de estabilidad emocional o vital."
                },
                new DreamAnalysisEntity
                {
                    Id = "dana2-drm2",
                    Title = "Análisis: Casa en Llamas",
                    DreamId = "drm2-p222l-vxztp-yub64-qm7fr-1298z",
                    EmotionalStateId = "ems9-p222l-vxztp-yub64-qm7fr-1298z",
                    Recommendation = "Explorar posibles fuentes de frustración interna relacionadas con el hogar o el entorno cercano.",
                    PatternBehaviour = "Reacción intensa ante eventos que escapan del control personal."
                },
                new DreamAnalysisEntity
                {
                    Id = "dana3-drm3",
                    Title = "Análisis: Mar Revuelto",
                    DreamId = "drm3-p222l-vxztp-yub64-qm7fr-1298z",
                    EmotionalStateId = "ems5-p222l-vxztp-yub64-qm7fr-1298z",
                    Recommendation = "Reflexionar sobre eventos recientes que hayan generado incertidumbre.",
                    PatternBehaviour = "Dificultad para adaptarse a cambios emocionales repentinos."
                },
                new DreamAnalysisEntity
                {
                    Id = "dana4-drm4",
                    Title = "Análisis: Persecución en la Ciudad",
                    DreamId = "drm4-p322l-vxztp-yub64-qm7fr-1298z",
                    EmotionalStateId = "ems1-p222l-vxztp-yub64-qm7fr-1298z",
                    Recommendation = "Aplicar ejercicios de respiración consciente ante situaciones de estrés.",
                    PatternBehaviour = "Tendencia a sentirse amenazado por factores externos o desconocidos."
                },
                new DreamAnalysisEntity
                {
                    Id = "dana5-drm5",
                    Title = "Análisis: Vuelo sin Control",
                    DreamId = "drm5-p322l-vxztp-yub64-qm7fr-1298z",
                    EmotionalStateId = "ems12-p222l-vxztp-yub64-qm7fr-1298z",
                    Recommendation = "Fomentar el enfoque positivo hacia metas personales, buscando equilibrio.",
                    PatternBehaviour = "Ambición elevada acompañada de inseguridad o falta de dirección clara."
                }
            );
        }
    }
}
