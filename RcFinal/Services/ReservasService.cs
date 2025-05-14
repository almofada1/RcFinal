using RcFinal.Models;

namespace RcFinal.Services;

public class ReservasService
{
    private readonly DapperContext _db;
    public ReservasService(DapperContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Reservas>> GetReservas()
    {
        var sql = "SELECT * FROM Reservas";
        return await _db.QueryAsync<Reservas>(sql);
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

        // Returns a single int
        var count = await _db.QuerySingleAsync<int>(sql, new
        {
            r.RoomId,
            r.CheckIn,
            r.CheckOut
        });  // <-- QuerySingleAsync<T> returns T :contentReference[oaicite:1]{index=1}

        return count > 0;
    }
    public async Task SaveReserva(Reservas reserva)
    {
        if (await HasOverlapAsync(reserva))
            throw new InvalidOperationException("That room is already booked for those dates.");

        var sql = @"
        INSERT INTO Reservas
            (CheckIn, CheckOut, Guests, RoomId, Email)
        VALUES
            (@CheckIn, @CheckOut, @Guests, @RoomId, @Email);";

        await _db.ExecuteAsync(sql, reserva);
    }
    public async Task DeleteReserva(int id)
    {
        var sql = "DELETE FROM Reservas WHERE Id = @Id";
        await _db.ExecuteAsync(sql, new { Id = id });
    }
    public async Task<IEnumerable<Quartos>> GetQuartosAsync()
    {
        var sql = "SELECT * FROM Quartos";
        return await _db.QueryAsync<Quartos>(sql);
    }
}

