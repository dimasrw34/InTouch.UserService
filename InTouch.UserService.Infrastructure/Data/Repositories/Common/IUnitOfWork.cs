
using InTouch.UserService.Core;

namespace InTouch.Infrastructure;


public interface IUnitOfWork : IDisposable
{
    void Commit();
    void Rollback();

    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
}