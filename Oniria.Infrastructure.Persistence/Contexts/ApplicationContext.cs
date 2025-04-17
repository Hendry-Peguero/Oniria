using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;
using Oniria.Infrastructure.Persistence.Seeds;

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
            SetPrimaryKeys(modelBuilder);
            SetTableNames(modelBuilder);
            SetRelationships(modelBuilder);
            Seeding(modelBuilder);
        }

        private void SetPrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<EmotionalStatesEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<DreamAnalysisEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<DreamEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<DreamTokenEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<EmotionalStatesEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<EmployeeEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<GenderEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MembershipAcquisitionEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MembershipBenefitEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MembershipBenefitRelationEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MembershipCategoryEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<MembershipEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<OrganizationEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<PatientEntity>().HasKey(p => p.Id);
        }

        private void SetTableNames(ModelBuilder modelBuilder)
        {
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
        }

        private void SetRelationships(ModelBuilder modelBuilder)
        {
            // Dream
            modelBuilder.Entity<DreamEntity>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Dreams)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Dream Analisys
            modelBuilder.Entity<DreamAnalysisEntity>()
                .HasOne(da => da.EmotionalState)
                .WithMany(es => es.DreamAnalyses)
                .HasForeignKey(da => da.EmotionalStateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DreamAnalysisEntity>()
                .HasOne(da => da.Dream)
                .WithOne(d => d.DreamAnalysis)
                .HasForeignKey<DreamAnalysisEntity>(da => da.DreamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Dream Token
            modelBuilder.Entity<DreamTokenEntity>()
                .HasOne(dt => dt.Patient)
                .WithOne(p => p.DreamToken)
                .HasForeignKey<DreamTokenEntity>(dt => dt.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Membership
            modelBuilder.Entity<MembershipEntity>()
                .HasOne(m => m.MembershipCategory)
                .WithMany(mc => mc.Memberships)
                .HasForeignKey(m => m.MembershipCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Membership Acquisition
            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Patient)
                .WithMany(p => p.MembershipAcquisitions)
                .HasForeignKey(ma => ma.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Organization)
                .WithMany(o => o.MembershipAcquisitions)
                .HasForeignKey(ma => ma.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MembershipAcquisitionEntity>()
                .HasOne(ma => ma.Membership)
                .WithMany(m => m.Acquisitions)
                .HasForeignKey(ma => ma.MembershipId)
                .OnDelete(DeleteBehavior.Cascade);

            // Membership Benefit Relation
            modelBuilder.Entity<MembershipBenefitRelationEntity>()
                .HasOne(mbr => mbr.Membership)
                .WithMany(m => m.BenefitRelations)
                .HasForeignKey(ma => ma.MembershipId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MembershipBenefitRelationEntity>()
                .HasOne(mbr => mbr.MembershipBenefit)
                .WithMany(mb => mb.BenefitRelations)
                .HasForeignKey(ma => ma.MembershipBenefitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Organization
            modelBuilder.Entity<OrganizationEntity>()
                .HasOne(o => o.EmployeeOwner)
                .WithMany(e => e.OrganizationOwned)
                .HasForeignKey(o => o.EmployeeOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient
            modelBuilder.Entity<PatientEntity>()
                .HasOne(p => p.Gender)
                .WithMany(g => g.Patients)
                .HasForeignKey(p => p.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientEntity>()
                .HasOne(p => p.Organization)
                .WithMany(o => o.Patients)
                .HasForeignKey(p => p.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee
            modelBuilder.Entity<EmployeeEntity>()
                .HasOne(e => e.OrganizationWhereWork)
                .WithMany(o => o.Employees)
                .HasForeignKey(p => p.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void Seeding(ModelBuilder modelBuilder)
        {
            // Defaults
            DefaultGenderSeeder.Seed(modelBuilder);
            DefaultMembershipCategorySeeder.Seed(modelBuilder);
            DefaultEmotionalStatesSeeder.Seed(modelBuilder);
            DefaultMembershipBenefitSeeder.Seed(modelBuilder);
            DefaultMembershipSeeder.Seed(modelBuilder);
            DefaultMembershipBenefitRelationSeeder.Seed(modelBuilder);

            //// Demo
            DemoEmployeeSeeder.Seed(modelBuilder);
            DemoOrganizationSeeder.Seed(modelBuilder);
            DemoPatientSeeder.Seed(modelBuilder);
            DemoMembershipAcquisitionSeeder.Seed(modelBuilder);
            DemoDreamTokenSeeder.Seed(modelBuilder);
            DemoDreamSeeder.Seed(modelBuilder);
            DemoDreamAnalysisSeeder.Seed(modelBuilder);
        }

    }
}
