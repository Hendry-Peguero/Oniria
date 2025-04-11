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

            // DreamAnalysisEntity
            modelBuilder.Entity<DreamAnalysisEntity>().HasKey(p => p.Id);

            // DreamEntity
            modelBuilder.Entity<DreamEntity>().HasKey(p => p.Id);

            // DreamTokenEntity
            modelBuilder.Entity<DreamTokenEntity>().HasKey(p => p.Id);

            // EmotionalStatesEntity
            modelBuilder.Entity<EmotionalStatesEntity>().HasKey(p => p.Id);

            // EmployeeEntity
            modelBuilder.Entity<EmployeeEntity>().HasKey(p => p.Id);

            // GenderEntity
            modelBuilder.Entity<GenderEntity>().HasKey(p => p.Id);

            // MembershipAcquisitionEntity
            modelBuilder.Entity<MembershipAcquisitionEntity>().HasKey(p => p.Id);

            // MembershipBenefitEntity
            modelBuilder.Entity<MembershipBenefitEntity>().HasKey(p => p.Id);

            // MembershipBenefitRelationEntity
            modelBuilder.Entity<MembershipBenefitRelationEntity>().HasKey(p => p.Id);

            // MembershipCategoryEntity
            modelBuilder.Entity<MembershipCategoryEntity>().HasKey(p => p.Id);

            // MembershipEntity
            modelBuilder.Entity<MembershipEntity>().HasKey(p => p.Id);

            // OrganizationEntity
            modelBuilder.Entity<OrganizationEntity>().HasKey(p => p.Id);

            // PatientEntity
            modelBuilder.Entity<PatientEntity>().HasKey(p => p.Id);

            #endregion

            #region Tables

            // Principal
            modelBuilder.Entity<GenderEntity>().ToTable("Genres");
            modelBuilder.Entity<EmotionalStatesEntity>().ToTable("EmotionalStates");
            modelBuilder.Entity<DreamAnalysisEntity>().ToTable("DreamAnalyses");
            modelBuilder.Entity<DreamEntity>().ToTable("Dreams");
            modelBuilder.Entity<DreamTokenEntity>().ToTable("DreamTokens");
            modelBuilder.Entity<EmotionalStatesEntity>().ToTable("EmotionalStates");
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");
            modelBuilder.Entity<GenderEntity>().ToTable("Genres");
            modelBuilder.Entity<MembershipAcquisitionEntity>().ToTable("MembershipAcquisitions");
            modelBuilder.Entity<MembershipBenefitEntity>().ToTable("MembershipBenefits");
            modelBuilder.Entity<MembershipBenefitRelationEntity>().ToTable("MembershipBenefitRelations");
            modelBuilder.Entity<MembershipCategoryEntity>().ToTable("MembershipCategories");
            modelBuilder.Entity<MembershipEntity>().ToTable("Memberships");
            modelBuilder.Entity<OrganizationEntity>().ToTable("Organizations");
            modelBuilder.Entity<PatientEntity>().ToTable("Patients");

            #endregion

            #region Relationships

            ////PropertyEntity y TypePropertyEntity
            //modelBuilder.Entity<PropertyEntity>()
            //    .HasOne(p => p.TypeProperty)
            //    .WithMany(tp => tp.Properties)
            //    .HasForeignKey(p => p.TypePropertyId)
            //    .OnDelete(DeleteBehavior.Cascade);

            #region Relationships

            // PatientEntity -> OrganizationEntity (muchos a uno)
            modelBuilder.Entity<PatientEntity>()
                .HasOne(p => p.Organization)
                .WithMany(o => o.Patients)
                .HasForeignKey(p => p.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            // PatientEntity -> GenderEntity (muchos a uno)
            modelBuilder.Entity<PatientEntity>()
                .HasOne(p => p.Gender)
                .WithMany()
                .HasForeignKey(p => p.GenderId)
                .OnDelete(DeleteBehavior.Cascade);

            // PatientEntity -> DreamEntity (uno a muchos)
            modelBuilder.Entity<DreamEntity>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Dreams)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade);  

            // PatientEntity -> DreamTokenEntity (uno a uno)
            modelBuilder.Entity<DreamTokenEntity>()
                .HasOne(dt => dt.Patient)
                .WithOne(p => p.DreamToken)
                .HasForeignKey<DreamTokenEntity>(dt => dt.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // PatientEntity -> MembershipAcquisitionEntity (uno a muchos)
            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Owner)
                .WithMany(p => p.MembershipAcquisitions)
                .HasForeignKey(ma => ma.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrganizationEntity -> EmployeeEntity (uno a muchos)
            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Employees)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);            
                 
            // OrganizationEntity -> MembershipAcquisitionEntity (uno a muchos)
            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Owner)
                .WithMany(o => o.MembershipAcquisitions)
                .HasForeignKey(ma => ma.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrganizationEntity -> EmployeeOwner (uno a uno)
            modelBuilder.Entity<OrganizationEntity>()
                .HasOne(o => o.EmployeeOwner)
                .WithOne()
                .HasForeignKey<OrganizationEntity>(o => o.EmployeeOwnerld)
                .OnDelete(DeleteBehavior.Cascade);

            // MembershipAcquisitionEntity -> MembershipEntity (muchos a uno)
            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Membership)
                .WithMany()
                .HasForeignKey(ma => ma.MembershipId)
                .OnDelete(DeleteBehavior.Cascade);

            // MembershipEntity -> MembershipCategoryEntity (muchos a uno)
            modelBuilder.Entity<MembershipEntity>()
                .HasOne(m => m.MembershipCategory)
                .WithMany()
                .HasForeignKey(m => m.MembershipCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // MembershipEntity -> MembershipBenefitRelationEntity (uno a muchos)
            modelBuilder.Entity<MembershipBenefitRelationEntity>()
                .HasOne(mbr => mbr.Membership)
                .WithMany(m => m.BenefitRelations)
                .HasForeignKey(mbr => mbr.MembershipId)
                .OnDelete(DeleteBehavior.Cascade);

            // MembershipBenefitRelationEntity -> MembershipBenefitEntity (muchos a uno)
            modelBuilder.Entity<MembershipBenefitRelationEntity>()
                .HasOne(mbr => mbr.MembershipBenefit)
                .WithMany()
                .HasForeignKey(mbr => mbr.MembershipBenefitId)
                .OnDelete(DeleteBehavior.Cascade);

            // DreamEntity -> DreamAnalysisEntity (uno a uno)
            modelBuilder.Entity<DreamEntity>()
                .HasOne(d => d.DreamAnalysis)
                .WithOne()
                .HasForeignKey<DreamEntity>(d => d.DreamAnalysisId)
                .OnDelete(DeleteBehavior.Cascade);

            // DreamAnalysisEntity -> EmotionalStatesEntity (muchos a uno)
            modelBuilder.Entity<DreamAnalysisEntity>()
                .HasOne(da => da.EmotionalStates)
                .WithMany()
                .HasForeignKey(da => da.EmotionalState)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion


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
