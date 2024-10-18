using System.Data;

namespace InTouch.Infrastructure.Data.Extensions;

public interface IDbTransactionAsync : IDbTransaction
{
    
    Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));
}