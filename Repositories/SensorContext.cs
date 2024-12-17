using backend_solar.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_solar.Repositories {
    public class SensorContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<DirtSensorData> DirtSensorData { get; set; }
        public DbSet<LuminositySensorData> LuminositySensorData { get; set; }
        public DbSet<TemperatureSensorData> TemperatureSensorData { get; set; }
        public DbSet<SolarPanelData> SolarPanelData { get; set; }

        public SensorContext(DbContextOptions<SensorContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Place>()
                .HasMany(u => u.AllowedUsers)
                .WithMany(u => u.UserPlaces);

            modelBuilder.Entity<Place>()
                .HasMany(u => u.Sensors);

            modelBuilder.Entity<DirtSensorData>()
                .HasKey(d => new { d.Id, d.Timestamp });

            modelBuilder.Entity<LuminositySensorData>()
                .HasKey(d => new { d.Id, d.Timestamp });

            modelBuilder.Entity<TemperatureSensorData>()
                .HasKey(d => new { d.Id, d.Timestamp });

            modelBuilder.Entity<SolarPanelData>()
                .HasKey(d => new { d.Id, d.Timestamp });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
