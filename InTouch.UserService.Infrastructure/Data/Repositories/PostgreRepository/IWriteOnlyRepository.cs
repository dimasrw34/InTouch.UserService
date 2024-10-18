using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public interface IWriteOnlyRepository<TEntity, in TKey> 
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<Guid> AddAsync(User entity);
}