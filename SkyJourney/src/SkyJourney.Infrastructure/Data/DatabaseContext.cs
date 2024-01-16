using Microsoft.EntityFrameworkCore;
using SkyJourney.Infrastructure.Data.Models;

namespace Amirez.Infrastructure.Data
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<FlightEntity> Flights { get; set; }
        public DbSet<PlanEntity> Plans { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<SeatArrangementEntity> SeatArrangements { get; set; }
        public DbSet<CityEntity> Cities { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightEntity>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");
            });
        }
    }
}
