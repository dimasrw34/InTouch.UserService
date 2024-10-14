using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public interface IUserWriteOnlyRepository : IWriteOnlyRepository
{
    Task ChangePasswordAsync(User entity);
}