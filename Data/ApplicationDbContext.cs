using CyclingTripManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CyclingTripManagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Participant> Participants { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripRegistration> TripRegistrations { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<TripRegistration>()
            .HasOne(tr => tr.Trip)
            .WithMany(t => t.TripRegistrations)
            .HasForeignKey(tr => tr.TripId);

        modelBuilder.Entity<TripRegistration>()
            .HasOne(tr => tr.Participant)
            .WithMany(p => p.TripRegistrations)
            .HasForeignKey(tr => tr.ParticipantId);

        modelBuilder.Entity<Hotel>()
            .HasOne(h => h.Trip)
            .WithMany(t => t.Hotels)
            .HasForeignKey(h => h.TripId);

        // Configure decimal precision for prices
        modelBuilder.Entity<Trip>()
            .Property(t => t.BasePrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Trip>()
            .Property(t => t.SingleRoomSupplement)
            .HasPrecision(18, 2);

        modelBuilder.Entity<TripRegistration>()
            .Property(tr => tr.TotalPrice)
            .HasPrecision(18, 2);

        // Seed some sample data
        modelBuilder.Entity<Trip>().HasData(
            new Trip
            {
                Id = 1,
                Name = "Alpine Adventure - Dolomites",
                Description = "Experience the breathtaking beauty of the Dolomites on this 7-day cycling adventure. Daily rides average 100km with approximately 2500 vertical meters. This challenging route takes you through stunning mountain passes, charming Italian villages, and offers spectacular views of the iconic Dolomite peaks.",
                StartDate = new DateTime(2026, 6, 15),
                EndDate = new DateTime(2026, 6, 21),
                Location = "Dolomites, Italy",
                DurationDays = 7,
                AverageDailyDistance = 100,
                AverageDailyVerticalMeters = 2500,
                BasePrice = 2500.00m,
                SingleRoomSupplement = 400.00m,
                MaxParticipants = 20,
                IsActive = true
            },
            new Trip
            {
                Id = 2,
                Name = "French Alps Explorer",
                Description = "Conquer legendary cols of the Tour de France on this epic 6-day journey through the French Alps. Experience iconic climbs like Alpe d'Huez and Col du Galibier while enjoying the finest French hospitality and cuisine.",
                StartDate = new DateTime(2026, 7, 10),
                EndDate = new DateTime(2026, 7, 15),
                Location = "French Alps, France",
                DurationDays = 6,
                AverageDailyDistance = 105,
                AverageDailyVerticalMeters = 2600,
                BasePrice = 2800.00m,
                SingleRoomSupplement = 450.00m,
                MaxParticipants = 18,
                IsActive = true
            }
        );
    }
}
