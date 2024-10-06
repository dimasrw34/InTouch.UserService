using InTouch.UserService.Core;

namespace InTouch.Infrastructure;

public interface IGenericRepository <in TEntity> where TEntity: BaseEntity
{
    Task InsertAsync(TEntity entity);
    
}