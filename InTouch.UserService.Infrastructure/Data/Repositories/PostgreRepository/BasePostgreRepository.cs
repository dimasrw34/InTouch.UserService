using Dapper;
using Npgsql;

namespace InTouch.Infrastructure.Data;

public abstract class BasePostgreRepository
{
    //protected readonly string _connectionString = configuration.GetConnectionString("PostgreSQL");
    //protected readonly ILogger _logger = logger;
    protected readonly string _connectionString = "Host=192.168.1.116;Database=postgres;Username=root;Password=example;Persist Security Info=True";
        
    public async Task ExecuteAsync(string sql, object param)
    {
        try
        {
            using var connection = GetConnection();
            await connection.QueryAsync(sql, param);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Ошибка ExecuteAsync");
            throw;
        }
    }

    public async IAsyncEnumerable<T> Query<T>(string sql, object param = null)
    {
        using var connection = GetConnection();
        var reader = await connection.ExecuteReaderAsync(sql, param);
        var rowParser = reader.GetRowParser<T>();

        while (await reader.ReadAsync())
        {
            yield return rowParser(reader);
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
    {
        try
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<T>(sql, param);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Ошибка QueryAsync");
            throw;
        }
    }
    public async Task<T> QuerySingleAsync<T>(string sql, object param = null)
    {
        try
        {
            using var connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
        catch (Exception ex)
        {
           // _logger.LogError(ex, "Ошибка QuerySingleAsync");
            throw;
        }
    }

    protected NpgsqlConnection GetConnection()
    {
        try
        {
            return new NpgsqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Ошибка соединения в BaseRepository");
            throw;
        }
    }
}