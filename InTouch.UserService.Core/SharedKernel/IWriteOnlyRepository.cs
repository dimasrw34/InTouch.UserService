namespace InTouch.UserService.Core;

/// <summary>
/// Представляет собой репозиторий, который допускает только операции записи для сущностей.
/// </summary>
/// <typeparam name="TEntity">Тип: Сущность.</typeparam>
/// <typeparam name="TKey">Тип: Ключ.</typeparam>
public interface IWriteOnlycRepository<TEntity, in TKey> : IDisposable
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Добавляет новую сущность в репозиторий.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Обновляет существующую сущность в репозитории.
    /// </summary>
    /// <param name="entity">Сущность для обновления.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность из репозитория.
    /// </summary>
    /// <param name="entity">Сущность для удаления</param>    
    void Remove(TEntity entity);

    /// <summary>
    /// Асинхронно извлекает сущность по ее идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности, которую необходимо получить.</param>
    /// <returns>Извлеченная сущность.</returns>
    Task<TEntity> GetByIdAsync(TKey id);
}