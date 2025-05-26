using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RcFinal.Models;

namespace RcFinal.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Quartos> Quartos { get; set; }
    public DbSet<Reservas> Reservas { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Estado> Estados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quartos>().ToTable("Quartos").HasData(
            new Quartos { RoomId = 1, Capacidade = 1 },
            new Quartos { RoomId = 2, Capacidade = 1 },
            new Quartos { RoomId = 3, Capacidade = 2 },
            new Quartos { RoomId = 4, Capacidade = 2 },
            new Quartos { RoomId = 5, Capacidade = 3 },
            new Quartos { RoomId = 6, Capacidade = 3 },
            new Quartos { RoomId = 7, Capacidade = 4 }
        );

        modelBuilder.Entity<Package>().ToTable("Packages").HasData(
            new Package { PackageId = "1", Name = "Room Only", PricePerNightPerGuest = 20 },
            new Package { PackageId = "2", Name = "Bed & Breakfast", PricePerNightPerGuest = 35 },
            new Package { PackageId = "3", Name = "Half Board", PricePerNightPerGuest = 50 },
            new Package { PackageId = "4", Name = "All-Inclusive", PricePerNightPerGuest = 60 }
        );

        modelBuilder.Entity<Reservas>()
            .HasOne(r => r.Estado)
            .WithMany(e => e.Reservas)
            .HasForeignKey(r => r.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Estado>().ToTable("Estados").HasData(
            new Estado { Id = 1, Nome = "Reserva" },
            new Estado { Id = 2, Nome = "Checked in" },
            new Estado { Id = 3, Nome = "Checked out" }
        );

        modelBuilder.Entity<Reservas>()
            .Property(r => r.EstadoId)
            .HasDefaultValue(1);

        base.OnModelCreating(modelBuilder);
    }
}
