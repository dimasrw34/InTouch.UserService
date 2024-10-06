namespace InTouch.UserService.Core;

/// <summary>
/// Представляет собой репозиторий для хранения событий в хранилище событий.
/// </summary>
public interface IEventStoreRepository : IDisposable
{
    /// <summary>
    /// Сохраняет коллекцию событий асинхронно.
    /// </summary>
    /// <param name="eventStores">Событие сохраняется в хранилище.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task StoreAsync(IEnumerable<EventStore> eventStores);
}