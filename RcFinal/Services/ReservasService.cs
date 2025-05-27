// Services/ReservasService.cs
using Microsoft.EntityFrameworkCore;
using RcFinal.Models;

namespace RcFinal.Services
{
    public class ReservasService
    {
        private readonly DapperContext _db;
        public ReservasService(DapperContext db) => _db = db;
        public async Task<int> FindAvailableRoomAsync(Reservas reserva)
        {
            // 1. Load all rooms matching the guest count
            var sqlRooms = "SELECT * FROM Quartos WHERE Capacidade = @Guests;";
            var rooms = (await _db.QueryAsync<Quartos>(sqlRooms, new { reserva.Guests })).ToList();

            // 2. Iterate and find the first non-overlapping reservation
            foreach (var room in rooms)
            {
                var overlapSql = @"
                    SELECT COUNT(1)
                    FROM Reservas
                    WHERE RoomId   = @RoomId
                      AND CheckIn  <  @CheckOut
                      AND CheckOut >  @CheckIn;
                 ";
                var count = await _db.QuerySingleAsync<int>
                (
                    overlapSql, new
                    {
                        room.RoomId,
                        reserva.CheckIn,
                        reserva.CheckOut
                    }
                 );

                if (count == 0)
                {
                    // no overlap → this room is free
                    return room.RoomId;
                }
            }

            // none free
            return -1;
        }
        public async Task<IEnumerable<Quartos>> GetQuartosAsync()
        {
            var sql = "SELECT * FROM Quartos";
            return await _db.QueryAsync<Quartos>(sql);
        }
        public async Task AddQuartoAsync(Quartos quarto)
        {
            var sql = "INSERT INTO Quartos (Capacidade) VALUES (@Capacidade);";
            await _db.ExecuteAsync(sql, new { quarto.Capacidade });
        }
        public async Task UpdateQuartoAsync(Quartos quarto)
        {
            var sql = "UPDATE Quartos SET Capacidade = @Capacidade WHERE RoomId = @RoomId;";
            await _db.ExecuteAsync(sql, new { quarto.Capacidade, quarto.RoomId });
        }
        public async Task<bool> DeleteQuartoAsync(int roomId)
        {
            var sql = "DELETE FROM Quartos WHERE RoomId = @RoomId;";
            var rowsAffected = await _db.ExecuteAsync(sql, new { RoomId = roomId });
            return rowsAffected > 0;
        }
        public async Task<List<Package>> GetPackagesAsync()
        {
            var sql = "SELECT * FROM Packages";
            var result = await _db.QueryAsync<Package>(sql);
            return [.. result];
        }
        public async Task<List<Reservas>> GetReservasByEmail(string email)
        {
            var sql = @"SELECT * FROM Reservas WHERE Email = @Email;";
            var result = await _db.QueryAsync<Reservas>(sql, new { Email = email });
            return [.. result];
        }
        public async Task<Reservas?> GetReservaByIdAsync(int id)
        {
            var sql = "SELECT * FROM Reservas WHERE Id = @Id;";
            return await _db.QuerySingleAsync<Reservas>(sql, new { Id = id });
        }
        public async Task UpdateReservaAsync(Reservas reserva)
        {
            var sql = @"
                UPDATE Reservas
                SET
                    CheckIn = @CheckIn,
                    CheckOut = @CheckOut,
                    Guests = @Guests,
                    Email = @Email,
                    RoomId = @RoomId,
                    PackageId = @PackageId,
                    TotalCost = @TotalCost,
                    EstadoId = @EstadoId
                WHERE Id = @Id;
            ";
            await _db.ExecuteAsync(sql, reserva);
        }
        public async Task<bool> HasOverlapAsync(Reservas r)
        {
            var sql = @"
                SELECT COUNT(1)
                FROM Reservas
                WHERE RoomId   = @RoomId
                  AND CheckIn  <  @CheckOut
                  AND CheckOut >  @CheckIn;
            ";
            var count = await _db.QuerySingleAsync<int>(sql, new { r.RoomId, r.CheckIn, r.CheckOut });
            return count > 0;
        }
        public async Task<int> SaveReserva(Reservas reserva)
        {
            //if (await HasOverlapAsync(reserva))
            //    throw new InvalidOperationException("That room is already booked for those dates.");

            var sql = @"
                INSERT INTO Reservas
                    (CheckIn, CheckOut, Guests, Email, RoomId, PackageId, TotalCost, EstadoId)
                VALUES
                    (@CheckIn, @CheckOut, @Guests, @Email, @RoomId, @PackageId, @TotalCost, @EstadoId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            // Returns the newly created PK
            var newId = await _db.QuerySingleAsync<int>(sql, reserva);
            return newId;
        }
        public decimal CalculateTotalCost(Reservas reserva, Package pkg)
        {
            var nights = (reserva.CheckOut - reserva.CheckIn).Days;
            if (nights < 1) throw new ArgumentException("Check-out must be at least one day after check-in.");

            return pkg.PricePerNightPerGuest
                 * reserva.Guests
                 * nights;
        }
        public async Task DeleteReserva(int id)
        {
            var sql = "DELETE FROM Reservas WHERE Id = @Id;";
            await _db.ExecuteAsync(sql, new { Id = id });
        }
        public async Task<List<Reservas>> GetBookingsForDateAsync(DateTime date)
        {
            var sql = @"SELECT * 
                FROM Reservas 
                WHERE CAST(CheckIn AS date) = @Date 
                   OR CAST(CheckOut AS date) = @Date;
                ";
            return (await _db.QueryAsync<Reservas>(sql, new { Date = date.Date })).ToList();
        }
        public async Task UpdateEstadoAsync(int reservaId, string newEstado)
        {
            var sql = "UPDATE Reservas SET Estado = @Estado WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = reservaId, Estado = newEstado });
        }
        public async Task<List<Estado>> GetEstadosAsync()
        {
            var sql = "SELECT * FROM Estados";
            var result = await _db.QueryAsync<Estado>(sql);
            return [.. result];
        }
    }
}
