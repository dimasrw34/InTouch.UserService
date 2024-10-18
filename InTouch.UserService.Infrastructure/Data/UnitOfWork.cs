using System.Data;
using MediatR;

using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public class UnitOfWork(
    IDbContext dbContext,
    IUserWriteOnlyRepository<User, Guid> userWriteOnlyRepository,
    IEventStoreRepository eventStoreRepository,
    IMediator mediator) : IUnitOfWork<User,Guid>
{
    private IDbTransactionAsync _transaction;
    private IDbConnectionAsync _connection;
    private IUserWriteOnlyRepository<User, Guid> Users => userWriteOnlyRepository;

    private IEventStoreRepository Events => eventStoreRepository;
    public async Task SaveChanges(User user, EventStore eventStore ,CancellationToken cancellationToken)
    {
        using (_connection is null? _connection = dbContext.ConnectionAsync: _connection)
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
            using (_transaction is null?_transaction = await _connection.BeginTransactionAsync(): _transaction)
            {
                try
                {
                    await Users.AddAsync(user);
                    await Events.StoreAsync(eventStore);
                    await _transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await _transaction.RollbackAsync();
                    Console.WriteLine(e);
                    throw;
                }
            }

            await _connection.CloseAsync();
        }
        foreach (var @event in user.DomainEvents)
        {
            await mediator.Publish(@event, cancellationToken);
        }
    }


    #region IDisposable

    // To detect redundant calls.
    private bool _disposed;

    // Public implementation of Dispose pattern callable by consumers.
    ~UnitOfWork() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose managed state (managed objects).
        if (disposing)
        {
            dbContext.Dispose();
        }

        _disposed = true;
    }

    #endregion
}