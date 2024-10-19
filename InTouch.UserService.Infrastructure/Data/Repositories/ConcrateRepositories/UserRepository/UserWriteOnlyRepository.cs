using System;
using System.Threading.Tasks;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public class UserWriteOnlyRepository (IDbContext context) :
                BaseWriteOnlyRepository(context), 
                IUserWriteOnlyRepository<User, Guid>
{
    public async Task<Guid> AddAsync(User user)
    {
                await ExecuteAsync(
                    @"CALL public.create_user (
                                @userid, 
                                @email, 
                                @password, 
                                @name, 
                                @surname, 
                                @phone)",
                    new { userid = user.Id,
                                email = user.Email.ToString(),
                                password = user.Password,
                                name = user.Name,
                                surname = user.Surname,
                                phone = user.Phone,
                                });
                return user.Id;
    }

    public Task<bool> ExistByEmailAsync(Email email)
    {
        throw new NotImplementedException();
    }
}