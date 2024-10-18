using System.Data;
using Npgsql;

namespace InTouch.Infrastructure.Data.Extensions;

public interface IDbConnectionAsync : IDbConnection
{
    Task OpenAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task CloseAsync();
    ValueTask<NpgsqlTransactionAsync> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
}