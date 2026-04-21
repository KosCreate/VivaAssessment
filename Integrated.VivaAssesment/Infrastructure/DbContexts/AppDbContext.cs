using Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts {
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<CountryEntity> Countries => Set<CountryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            ConfigureCountries(modelBuilder);
        }

        private static void ConfigureCountries(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CountryEntity>(entity => {
                entity.ToTable("Countries");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.CommonName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Capital)
                    .HasMaxLength(200);

                entity.Property(x => x.Borders);

                entity.HasIndex(x => x.CommonName)
                    .IsUnique();
            });
        }
    }
}
