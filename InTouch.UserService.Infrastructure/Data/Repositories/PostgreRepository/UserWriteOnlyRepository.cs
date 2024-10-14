using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public class UserWriteOnlyRepository : BasePostgreRepository, IUserWriteOnlyRepository
{
    public async Task<Guid> AddAsync(BaseEntity baseEntity)
    {
        
        Console.WriteLine("Сохраняем сущность в PostreSQL");
        var user = baseEntity as User;
        
        var _sql = @"CALL public.create_user(@userid, @email, @password, @name, @surname, @phone)";
        
        var _param = new
        {
            userid = user.Id,
            email = user.Email.ToString(),
            password = user.Password,
            name = user.Name,
            surname = user.Surname,
            phone = user.Phone,
        };
        //сохраняем в базу
        await ExecuteAsync(_sql, _param);
        

        //После сохранения, нужно сделать EventStore
        var eventRepository = new EventRepository();
        
        var eventStore = new EventStore(
            user.Id,
            "CreateUserEntity",
            user.ToJson());
        
        await eventRepository.StoreAsync(eventStore);
        
        return user.Id;
    }

    public async Task ChangePasswordAsync(User entity)
    {
        return;
    }
}