using Microsoft.EntityFrameworkCore;
using Kolokwium2.Models;

namespace Kolokwium2.Data;

public class AppDbContext : DbContext
{
    public DbSet<Racer> Racers { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<RaceParticipation>().HasKey(Rp => new { Rp.RacerId, Rp.TrackRaceId });

        //Racer -> RaceParticipation
        modelBuilder.Entity<Racer>()
            .HasMany(r => r.RaceParticipations)
            .WithOne(rp => rp.Racer)
            .HasForeignKey(rp => rp.RacerId);

        //TrackRace -> RaceParticipation
        modelBuilder.Entity<TrackRace>()
            .HasMany(tr => tr.RaceParticipations)
            .WithOne(rp => rp.TrackRace)
            .HasForeignKey(rp => rp.TrackRaceId);

        //Track -> TrackRace
        modelBuilder.Entity<Track>()
            .HasMany(t => t.TrackRaces)
            .WithOne(tr => tr.Track)
            .HasForeignKey(tr => tr.TrackId);

        //Race -> TrackRace
        modelBuilder.Entity<Race>()
            .HasMany(r => r.TrackRaces)
            .WithOne(tr => tr.Race)
            .HasForeignKey(tr => tr.RaceId);
            
        
        modelBuilder.Entity<Racer>().HasData(
            new Racer
            {
                RacerId = 1,
                FirstName = "Lewis",
                LastName = "Hamilton"
            }
        );

        modelBuilder.Entity<Track>().HasData(
            new Track
            {
                TrackId = 1,
                Name = "Silverstone Circuit",
                LengthInKm = 5.89
            },
            new Track
            {
                TrackId = 2,
                Name = "Monaco Circuit",
                LengthInKm = 3.34
            }
        );

        modelBuilder.Entity<Race>().HasData(
            new Race
            {
                RaceId = 1,
                Name = "British Grand Prix",
                Location = "Silverstone, UK",
                Date = new DateTime(2025, 7, 14)
            },
            new Race
            {
                RaceId = 2,
                Name = "Monaco Grand Prix",
                Location = "Monte Carlo, Monaco",
                Date = new DateTime(2025, 5, 25)
            }
        );

        modelBuilder.Entity<TrackRace>().HasData(
            new TrackRace
            {
                TrackRaceId = 1,
                TrackId = 1,
                RaceId = 1,
                Laps = 52,
                BestTimeInSeconds = null
            },
            new TrackRace
            {
                TrackRaceId = 2,
                TrackId = 2,
                RaceId = 2,
                Laps = 78,
                BestTimeInSeconds = null
            }
        );

        modelBuilder.Entity<RaceParticipation>().HasData(

            new RaceParticipation
            {
                RacerId = 1,
                TrackRaceId = 1,
                FinishTimeInSeconds = 5460,
                Position = 1,
            },
            new RaceParticipation
            {
                RacerId = 1,
                TrackRaceId = 2,
                FinishTimeInSeconds = 6300,
                Position = 2
            }
        );
    }
}