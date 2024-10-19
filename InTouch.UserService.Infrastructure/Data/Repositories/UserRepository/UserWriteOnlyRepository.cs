using System;
using System.Data;
using System.Threading.Tasks;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public class UserWriteOnlyRepository (IDbContext context) :
                BaseWriteOnlyRepository(context.Connection), 
                IUserWriteOnlyRepository<User, Guid>
{
    private readonly IDbConnection _connection = context.Connection;
    private readonly IDbTransaction _transaction = context.Transaction;
    public async Task<Guid> AddAsync(User user)
    {
                await ExecuteAsync(
                    @"BEGIN; INSERT INTO public.users (id, email, password, name, surname, phone) 
                                        VALUES (@userid,  @email, @password,@name, @surname, @phone);",
                    _transaction,
                    new { userid = user.Id,
                                email = user.Email.ToString(),
                                password = user.Password,
                                name = user.Name,
                                surname = user.Surname,
                                phone = user.Phone,
                                });
                return user.Id;
    }

    public async Task<bool> ExistByEmailAsync(Email email) =>
        await QuerySingleAsync<bool>(@"SELECT public.check_user_login (@email)",
                                 _transaction,
                        new {email = email.Address});
}