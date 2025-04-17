using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Seeds
{
    public static class DefaultMembershipBenefitRelationSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembershipBenefitRelationEntity>().HasData(
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB3", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB4", Status = StatusEntity.ACTIVE },

                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS3", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS4", Status = StatusEntity.ACTIVE },

                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP3", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP4", Status = StatusEntity.ACTIVE },

                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB3", Status = StatusEntity.ACTIVE },

                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS3", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS4", Status = StatusEntity.ACTIVE },

                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP1", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP2", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP3", Status = StatusEntity.ACTIVE },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP4", Status = StatusEntity.ACTIVE }
            );
        }
    }
}
