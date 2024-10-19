using System;
using System.Threading.Tasks;

using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public interface IUserWriteOnlyRepository<TEntity, in TKey> : 
    IWriteOnlyRepository<TEntity, TKey> 
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    Task<bool> ExistByEmailAsync(Email email);
}