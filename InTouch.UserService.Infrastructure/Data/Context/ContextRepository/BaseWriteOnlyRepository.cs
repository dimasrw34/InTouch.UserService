using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace InTouch.Infrastructure.Data;

public abstract class BaseWriteOnlyRepository(IDbContext context) 
{
    private readonly IDbConnection _connection = context.Connection;
    private readonly IDbTransaction _transaction = context.Transaction;
    
    public async Task ExecuteAsync(string sql, object param)
        => await  _connection.QueryAsync(sql, param, _transaction);
    

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null) =>
        await _connection.QueryAsync<T>(sql, param);
        
    public async Task<T> QuerySingleAsync<T>(string sql,  object? param = null) =>
        await _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction);
}