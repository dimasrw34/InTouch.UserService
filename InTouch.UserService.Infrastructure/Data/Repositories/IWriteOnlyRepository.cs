using System;
using System.Threading.Tasks;
using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

/// <summary>
/// General repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IWriteOnlyRepository<TEntity, in TKey> 
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<Guid> AddAsync(TEntity entity);
    /*
    Task<Guid> UpdateAsync(TEntity entity);
    Task<Guid> DeleteSoftAsync(TEntity entity);
    Task<Guid> DeleteAsync(TEntity entity);
    */
}