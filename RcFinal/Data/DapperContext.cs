using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.Common;

public class DapperContext
{
    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
        => _connectionString = configuration.GetConnectionString("DefaultConnection");

    public DbConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();
        return await conn.QueryAsync<T>(sql, parameters);
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();
        return await conn.ExecuteAsync(sql, parameters);
    }
}
