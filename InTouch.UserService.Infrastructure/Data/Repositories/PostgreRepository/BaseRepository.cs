using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace InTouch.Infrastructure;

public class BaseRepository(IConfiguration configuraton)
{
    protected readonly string _connectionString = configuraton.GetConnectionString("PostgreSQL");

    protected NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);

    public async Task<T> QuerySingleAsync<T>(string sql, object param = null)  
    {
        using var _connection = GetConnection();
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }
}