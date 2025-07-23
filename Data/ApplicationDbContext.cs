using EduCoreSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace EduCoreSuite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Institution> Institutions { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<SubCounty> SubCounties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Counties
            modelBuilder.Entity<County>().HasData(
                new County { CountyID = 1, CountyName = "Nairobi" },
                new County { CountyID = 2, CountyName = "Mombasa" }
            );

            // Seed SubCounties
            modelBuilder.Entity<SubCounty>().HasData(
                new SubCounty { SubCountyID = 1, SubCountyName = "Westlands", CountyID = 1 },
                new SubCounty { SubCountyID = 2, SubCountyName = "Lang'ata", CountyID = 1 },
                new SubCounty { SubCountyID = 3, SubCountyName = "Likoni", CountyID = 2 }
            );
        }
    }
}
