using System.Data;
using InTouch.Infrastructure.Data.Extensions;

namespace InTouch.Infrastructure.Data;

public interface IDbContext
{
    NpgsqlConnectionAsync ConnectionAsync { get; }
   NpgsqlTransactionAsync TransactionAsync { get; }
    void Dispose();
}