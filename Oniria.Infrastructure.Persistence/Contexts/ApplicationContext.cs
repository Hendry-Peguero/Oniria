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
                .HasForeignKey(o => o.EmployeeOwnerld)
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
            modelBuilder.Entity<GenderEntity>().HasData(
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Hombre", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Mujer", Status = StatusEntity.ACTIVE },
                new GenderEntity { Id = GeneratorHelper.GuidString(), Description = "Perfiero no decirlo", Status = StatusEntity.ACTIVE }
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

            modelBuilder.Entity<MembershipBenefitEntity>().HasData(
                // Patient Basic
                new MembershipBenefitEntity { Id = "PB1", Description = "Limited dream recording & analysis", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB2", Description = "General recommendations", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB3", Description = "Summarized history", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PB4", Description = "Essential features", Status = StatusEntity.ACTIVE },

                // Patient Standard
                new MembershipBenefitEntity { Id = "PS1", Description = "Increased dream recording", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS2", Description = "Personalized analysis & suggestions", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS3", Description = "Evolution statistics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PS4", Description = "Enhanced profile & support", Status = StatusEntity.ACTIVE },

                // Patient Premium
                new MembershipBenefitEntity { Id = "PP1", Description = "Unlimited dream recording", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP2", Description = "Advanced, real-time analysis", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP3", Description = "Wearable integration", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "PP4", Description = "Priority support & exclusive updates", Status = StatusEntity.ACTIVE },

                // Organization Basic
                new MembershipBenefitEntity { Id = "OB1", Description = "Manage up to 20 users", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OB2", Description = "Basic dashboard metrics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OB3", Description = "Essential administration features", Status = StatusEntity.ACTIVE },

                // Organization Standard
                new MembershipBenefitEntity { Id = "OS1", Description = "Manage up to 100 users", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS2", Description = "Advanced dashboard with detailed reports", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS3", Description = "Basic system integration", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OS4", Description = "Dedicated support", Status = StatusEntity.ACTIVE },

                // Organization Premium
                new MembershipBenefitEntity { Id = "OP1", Description = "Unlimited user management", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP2", Description = "Customizable reports & analytics", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP3", Description = "Full integration with internal systems", Status = StatusEntity.ACTIVE },
                new MembershipBenefitEntity { Id = "OP4", Description = "Priority support & consultancy", Status = StatusEntity.ACTIVE }
            );

            modelBuilder.Entity<MembershipCategoryEntity>().HasData(
                new MembershipCategoryEntity { Id = "P", Description = "Patient", Status = StatusEntity.ACTIVE },
                new MembershipCategoryEntity { Id = "O", Description = "Organization", Status = StatusEntity.ACTIVE }
            );

            modelBuilder.Entity<MembershipEntity>().HasData(
                // Patient Memberships
                new MembershipEntity
                {
                    Id = "PB",
                    Price = 9.99,
                    Description = "Basic",
                    DurationDays = 30,
                    MembershipCategoryId = "P"
                },
                new MembershipEntity
                {
                    Id = "PS",
                    Price = 19.99,
                    Description = "Standard",
                    DurationDays = 90,
                    MembershipCategoryId = "P"
                },
                new MembershipEntity
                {
                    Id = "PP",
                    Price = 29.99,
                    Description = "Premium",
                    DurationDays = 365,
                    MembershipCategoryId = "P"
                },

                // Organization Memberships
                new MembershipEntity
                {
                    Id = "OB",
                    Price = 49.99,
                    Description = "Basic",
                    DurationDays = 30,
                    MembershipCategoryId = "O"
                },
                new MembershipEntity
                {
                    Id = "OS",
                    Price = 99.99,
                    Description = "Standard",
                    DurationDays = 90,
                    MembershipCategoryId = "O"
                },
                new MembershipEntity
                {
                    Id = "OP",
                    Price = 199.99,
                    Description = "Premium",
                    DurationDays = 365,
                    MembershipCategoryId = "O"
                }
            );

            modelBuilder.Entity<MembershipBenefitRelationEntity>().HasData(
                // Patient Basic Membership (PB)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB3" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PB", MembershipBenefitId = "PB4" },

                // Patient Standard Membership (PS)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS3" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PS", MembershipBenefitId = "PS4" },

                // Patient Premium Membership (PP)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP3" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "PP", MembershipBenefitId = "PP4" },

                // Organization Basic Membership (OB)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OB", MembershipBenefitId = "OB3" },

                // Organization Standard Membership (OS)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS3" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OS", MembershipBenefitId = "OS4" },

                // Organization Premium Membership (OP)
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP1" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP2" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP3" },
                new MembershipBenefitRelationEntity { Id = GeneratorHelper.GuidString(), Available = true, MembershipId = "OP", MembershipBenefitId = "OP4" }
            );

        }

    }
}
