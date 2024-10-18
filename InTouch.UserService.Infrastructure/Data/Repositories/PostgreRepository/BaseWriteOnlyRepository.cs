using System.Data;
using Dapper;
using InTouch.Infrastructure.Data.Extensions;

namespace InTouch.Infrastructure.Data;

public abstract class BaseWriteOnlyRepository(IDbContext context) 
{
    private readonly IDbConnectionAsync _connection = context.ConnectionAsync;
    private readonly IDbTransactionAsync _transaction = context.TransactionAsync;
    
    public async Task ExecuteAsync(string sql, object param) =>
        await _connection.QueryAsync(sql, param, _transaction);

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null) =>
        await _connection.QueryAsync<T>(sql, param);
        
    public async Task<T> QuerySingleAsync<T>(string sql,  object? param = null) =>
        await _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction);
}