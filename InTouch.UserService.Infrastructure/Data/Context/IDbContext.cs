namespace InTouch.Infrastructure.Data;

public interface IDbContext
{
    IDbConnectionAsync ConnectionAsync { get; }
    IDbTransactionAsync TransactionAsync { get; }
    void Dispose();
}