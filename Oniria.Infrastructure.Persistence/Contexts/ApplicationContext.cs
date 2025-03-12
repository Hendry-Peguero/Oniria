using Microsoft.EntityFrameworkCore;

namespace DreamHouse.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        // Entities
        //public DbSet<PropertyFavoriteEntity> PropertyFavorites { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Primary Keys

            // Principal
            //modelBuilder.Entity<PropertyEntity>().HasKey(p => p.Id);

            #endregion

            #region Tables

            // Principal
            //modelBuilder.Entity<PropertyEntity>().ToTable("Properties");

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

            ////ImprovementEntity
            //modelBuilder.Entity<ImprovementEntity>().HasData(
            //    new ImprovementEntity { Id = 1, Name = "Renovación de Cocina", Description = "Renovación completa de la cocina incluyendo electrodomésticos" },
            //    new ImprovementEntity { Id = 2, Name = "Renovación de Baño", Description = "Renovación completa del baño incluyendo nuevos accesorios" },
            //    new ImprovementEntity { Id = 3, Name = "Ampliación de Sala", Description = "Ampliación de la sala con nuevo mobiliario" },
            //    new ImprovementEntity { Id = 4, Name = "Renovación de Jardín", Description = "Rediseño y renovación del jardín incluyendo nuevas plantas" },
            //    new ImprovementEntity { Id = 5, Name = "Pintura Exterior", Description = "Pintura de la fachada exterior de la casa" },
            //    new ImprovementEntity { Id = 6, Name = "Renovación de Cocina2", Description = "Renovación completa de la cocina incluyendo electrodomésticos2" },
            //    new ImprovementEntity { Id = 7, Name = "Renovación de Baño2", Description = "Renovación completa del baño incluyendo nuevos accesorios2" },
            //    new ImprovementEntity { Id = 8, Name = "Ampliación de Sala2", Description = "Ampliación de la sala con nuevo mobiliario2" },
            //    new ImprovementEntity { Id = 9, Name = "Renovación de Jardín2", Description = "Rediseño y renovación del jardín incluyendo nuevas plantas2" },
            //    new ImprovementEntity { Id = 10, Name = "Pintura Exterior2", Description = "Pintura de la fachada exterior de la casa2" }
            //);

            #endregion
        }
    }
}
