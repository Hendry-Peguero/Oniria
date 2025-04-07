using Microsoft.EntityFrameworkCore;
using Oniria.Core.Application.Helpers;
using Oniria.Core.Domain.Entities;
using Oniria.Core.Domain.Enums;

namespace Oniria.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        // Entities
        public DbSet<DreamAnalysisEntity> DreamAnalyses { get; set; }
        public DbSet<DreamEntity> Dreams { get; set; }
        public DbSet<DreamTokenEntity> DreamTokens { get; set; }
        public DbSet<EmotionalStatesEntity> EmotionalStates { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<EntityAudit> EntityAudits { get; set; }
        public DbSet<GenderEntity> Genders { get; set; }
        public DbSet<MembershipAcquisitionEntity> MembershipAcquisitions { get; set; }
        public DbSet<MembershipBenefitEntity> MembershipBenefits { get; set; }
        public DbSet<MembershipBenefitRelationEntity> MembershipBenefitRelations { get; set; }
        public DbSet<MembershipCategoryEntity> MembershipCategories { get; set; }
        public DbSet<MembershipEntity> Memberships { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Primary Keys

            // Principal
            modelBuilder.Entity<GenderEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<EmotionalStatesEntity>().HasKey(p => p.Id);

            #endregion

            #region Tables

            // Principal
            modelBuilder.Entity<GenderEntity>().ToTable("Genres");
            modelBuilder.Entity<EmotionalStatesEntity>().ToTable("EmotionalStates");

            #endregion

            #region Relationships

            ////PropertyEntity y TypePropertyEntity
            //modelBuilder.Entity<PropertyEntity>()
            //    .HasOne(p => p.TypeProperty)
            //    .WithMany(tp => tp.Properties)
            //    .HasForeignKey(p => p.TypePropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Seedings

            modelBuilder.Entity<GenderEntity>().HasData(
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Hombre", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Mujer", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Ninguno", Status = StatusEntity.ACTIVE }
            );

            modelBuilder.Entity<EmotionalStatesEntity>().HasData(
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Ansiedad", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Felicidad", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Tristeza", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Miedo", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Confusión", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Culpa", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Vergüenza", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Euforia", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Ira", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Amor", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Soledad", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Esperanza", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Relajación", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Sorpresa", Status = StatusEntity.ACTIVE },
                new EmotionalStatesEntity { Id = GeneratorHelper.GuidString(), Description = "Gratitud", Status = StatusEntity.ACTIVE }
            );

            #endregion
        }
    }
}
