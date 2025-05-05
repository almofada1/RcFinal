using RcFinal.Data;

namespace RcFinal.Services
{
    public class ReservasService
    {
        private readonly DapperContext _db;
        public ReservasService(DapperContext db) => _db = db;

        public async Task<bool> SubmiteAsync()
        {
            return true;
        }
    }
}
