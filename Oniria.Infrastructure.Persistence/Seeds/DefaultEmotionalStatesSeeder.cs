using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultEmotionalStatesSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmotionalStatesEntity>().HasData(
                new EmotionalStatesEntity { Id = "ems1-p222l-vxztp-yub64-qm7fr-1298z", Description = "Anxiety", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems2-p222l-vxztp-yub64-qm7fr-1298z", Description = "Happiness", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems3-p222l-vxztp-yub64-qm7fr-1298z", Description = "Sadness", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems4-p222l-vxztp-yub64-qm7fr-1298z", Description = "Fear", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems5-p222l-vxztp-yub64-qm7fr-1298z", Description = "Confusion", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems6-p222l-vxztp-yub64-qm7fr-1298z", Description = "Guilt", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems7-p222l-vxztp-yub64-qm7fr-1298z", Description = "Shame", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems8-p222l-vxztp-yub64-qm7fr-1298z", Description = "Euphoria", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems9-p222l-vxztp-yub64-qm7fr-1298z", Description = "Anger", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems10-p222l-vxztp-yub64-qm7fr-1298z", Description = "Love", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems11-p222l-vxztp-yub64-qm7fr-1298z", Description = "Loneliness", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems12-p222l-vxztp-yub64-qm7fr-1298z", Description = "Hope", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems13-p222l-vxztp-yub64-qm7fr-1298z", Description = "Relaxation", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems14-p222l-vxztp-yub64-qm7fr-1298z", Description = "Surprise", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = "ems15-p222l-vxztp-yub64-qm7fr-1298z", Description = "Gratitude", Status = StatusEntity.ACTIVE }
            );
        }
    }
}
