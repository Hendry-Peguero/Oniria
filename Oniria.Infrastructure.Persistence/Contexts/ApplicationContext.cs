using Microsoft.EntityFrameworkCore;
using Oniria.Core.Domain.Entities;

namespace Oniria.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        // Entities
        public DbSet<GenderEntity> Genders { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Primary Keys

            // Principal
            modelBuilder.Entity<GenderEntity>().HasKey(p => p.Id);

            #endregion

            #region Tables

            // Principal
            modelBuilder.Entity<GenderEntity>().ToTable("Genres");

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
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "Hombre" },
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "Mujer" },
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "Helicopter" },
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "Tanque de Guerra" },
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "MMGVO" },
                new GenderEntity { Id = Guid.NewGuid().ToString(), Description = "Enero" }
            );

            #endregion
        }
    }
}
