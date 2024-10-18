using System.Data;
using Npgsql;

namespace InTouch.Infrastructure.Data;

public class NpgsqlTransactionAsync (ValueTask<NpgsqlTransaction> transaction): IDbTransactionAsync 
{
    private readonly ValueTask<NpgsqlTransaction> _transaction = transaction;
    public void Dispose() => _transaction.Result.Dispose();
    
    public void Commit() => _transaction.Result.Commit();
    
    public void Rollback() => _transaction.Result.Rollback();

    public IDbConnection? Connection { get; }
    
    public IsolationLevel IsolationLevel { get; }

    public IDbConnectionAsync ConnectionAsync { get; set; }

    public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
        await _transaction.Result.CommitAsync(cancellationToken);
    public async Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
        await _transaction.Result.CommitAsync(cancellationToken);
    
    public async Task DisposeAsync(CancellationToken cancellationToken = default) =>
          _transaction.Result.DisposeAsync();

}