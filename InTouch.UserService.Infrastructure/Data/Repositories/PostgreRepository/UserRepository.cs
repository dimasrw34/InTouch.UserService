using Microsoft.Extensions.Configuration;

namespace InTouch.Infrastructure;

public class UserRepository<TEntity> (IConfiguration configuration)
    : BaseRepository(configuration), IUserRepository
{
    public async Task CreateAsync()
    {
        var _sqlString = @"SELECT * FROM";
        var id = await QuerySingleAsync<int>(_sqlString);
    }
}