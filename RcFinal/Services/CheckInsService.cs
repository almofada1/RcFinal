using RcFinal.Data;

namespace RcFinal.Services
{
    public class CheckInsService
    {
        private readonly DapperContext _db;
        public CheckInsService(DapperContext db) => _db = db;

        public async Task<string[]> GetUsers()
        {
            string sql = "SELECT UserName FROM AspNetUsers";
            var result = await _db.QueryAsync<string>(sql);
            return result.ToArray();
        }
    }
}
