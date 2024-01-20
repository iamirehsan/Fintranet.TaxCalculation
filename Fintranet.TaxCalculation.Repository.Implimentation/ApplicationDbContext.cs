using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculation.Repository.Implimentation
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<InitialSetting> InitialSetting { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleTaxDate> VehicleTaxDate { get; set; }
        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTaxDateConfiguration());
            modelBuilder.ApplyConfiguration(new InitialSettingConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());


        }




    }
}
