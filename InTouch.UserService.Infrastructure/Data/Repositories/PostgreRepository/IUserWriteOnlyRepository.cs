using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public interface IUserWriteOnlyRepository : IWriteOnlyRepository
{
    Task ChangePasswordAsync(BaseEntity entity);
}