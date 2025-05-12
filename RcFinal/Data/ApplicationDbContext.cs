using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RcFinal.Models;

namespace RcFinal.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservas> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed some experimental rooms
        modelBuilder.Entity<Room>().HasData(
            new Room { RoomId = 8, Capacity = 2 },
            new Room { RoomId = 9, Capacity = 3 },
            new Room { RoomId = 10, Capacity = 4 }
        );

        base.OnModelCreating(modelBuilder);
    }
}
