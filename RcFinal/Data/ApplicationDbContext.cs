using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RcFinal.Models;

namespace RcFinal.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Quartos> Quartos { get; set; }
    public DbSet<Reservas> Reservas { get; set; }
    public DbSet<Package> Packages { get; set; }

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

        base.OnModelCreating(modelBuilder);
    }
}
