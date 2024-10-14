using InTouch.UserService.Core;
using InTouch.UserService.Domain;
using MediatR;

namespace InTouch.Infrastructure.Data;

//IEventStoreRepository eventStoreRepository
public class UnitOfWork (IMediator mediator) : IUnitOfWork
{
    public async Task SaveChanges(BaseEntity entity, CancellationToken cancellationToken)
    {
        foreach (var @event in entity.DomainEvents)
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
            //writeDbContext.Dispose();
            //eventStoreRepository.Dispose();
        }

        _disposed = true;
    }

    #endregion
}