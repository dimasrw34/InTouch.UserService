using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public interface IWriteOnlyRepository<TEntity, in TKey> 
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<Guid> AddAsync(TEntity entity);
}