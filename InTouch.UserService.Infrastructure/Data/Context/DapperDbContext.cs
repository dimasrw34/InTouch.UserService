using System.Data;
using InTouch.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace InTouch.Infrastructure.Data;

public class DapperDbContext : IDbContext
{
    private readonly NpgsqlConnectionAsync _connection;
    private readonly NpgsqlTransactionAsync _transaction;
    
    private readonly NpgsqlConnection _connectionNpg;
    private readonly NpgsqlTransaction _transactionNpg;
    
    public DapperDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        _connection = new NpgsqlConnectionAsync(new NpgsqlConnection(connectionString));
    }
    public NpgsqlConnectionAsync ConnectionAsync => _connection;
    public NpgsqlTransactionAsync TransactionAsync => _transaction;

    public void Dispose() =>  _connection?.Dispose();

}