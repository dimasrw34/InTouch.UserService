namespace InTouch.UserService.Core;

/// <summary>
/// Представляет собой единицу работы по управлению операциями базы данных.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Сохраняет изменения, внесенные в единицу работы, асинхронно.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task SaveChanges();
}