using System.Data;

namespace InTouch.Infrastructure.Data;

public interface IDbConnectionAsync : IDbConnection
{
    Task OpenAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task CloseAsync();
    ValueTask<NpgsqlTransactionAsync> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
}