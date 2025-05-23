// Services/ReservasService.cs
using RcFinal.Models;
using Dapper;

namespace RcFinal.Services
{
    public class ReservasService
    {
        private readonly DapperContext _db;
        public ReservasService(DapperContext db) => _db = db;

        public async Task<IEnumerable<Quartos>> GetQuartosAsync()
        {
            var sql = "SELECT * FROM Quartos";
            return await _db.QueryAsync<Quartos>(sql);
        }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            var sql = "SELECT * FROM Packages";
            return await _db.QueryAsync<Package>(sql);
        }

        public async Task<IEnumerable<Reservas>> GetReservasByEmail(string email)
        {
            var sql = @"
                SELECT * FROM Reservas 
                WHERE Email = @Email;";
            return await _db.QueryAsync<Reservas>(sql, new { Email = email });
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

        /// <summary>
        /// Inserts a reservation and returns its new Id.
        /// </summary>
        public async Task<int> SaveReserva(Reservas reserva)
        {
            if (await HasOverlapAsync(reserva))
                throw new InvalidOperationException("That room is already booked for those dates.");

            var sql = @"
                INSERT INTO Reservas
                    (CheckIn, CheckOut, Guests, Email, RoomId, PackageId, TotalCost)
                VALUES
                    (@CheckIn, @CheckOut, @Guests, @Email, @RoomId, @PackageId, @TotalCost);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            // Returns the newly created PK
            var newId = await _db.QuerySingleAsync<int>(sql, reserva);
            return newId;
        }

        public async Task SaveReservationCost(ReservationCost costEntry)
        {
            var sql = @"
                INSERT INTO ReservationCost
                    (ReservaId, Cost, RecordedAt)
                VALUES
                    (@ReservaId, @Cost, @RecordedAt);
            ";
            await _db.ExecuteAsync(sql, costEntry);
        }

        public async Task DeleteReserva(int id)
        {
            var sql = "DELETE FROM Reservas WHERE Id = @Id;";
            await _db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
