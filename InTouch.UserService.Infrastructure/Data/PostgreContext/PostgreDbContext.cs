using Microsoft.Extensions.Configuration;
using Npgsql;

namespace InTouch.Infrastructure.Data;

public class PostgreDbContext : IDbContext
{
    private readonly NpgsqlConnectionAsync _connection;
    private readonly NpgsqlTransactionAsync _transaction;
    
    private readonly NpgsqlConnection _connectionNpg;
    private readonly NpgsqlTransaction _transactionNpg;
    
    public PostgreDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        _connection = new NpgsqlConnectionAsync(new NpgsqlConnection(connectionString));
    }
    public IDbConnectionAsync ConnectionAsync => _connection;
    public IDbTransactionAsync TransactionAsync => _transaction;

    public void Dispose() =>  _connection?.Dispose();

}