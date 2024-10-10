using System.Threading.Tasks.Dataflow;
using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public class UserWriteOnlyRepository : BasePostgreRepository, IUserWriteOnlyRepository
{
    public async Task<Guid> AddAsync(BaseEntity baseEntity)
    {
        var user =  baseEntity as User;
        
        //После сохранения, нужно сделать EventStore
        var eventRepository = new EventRepository();
        
        var bb = new EventStore(
            user.Id,
            "Create entity",
            user.ToJson());
        
        
        
        eventRepository.StoreAsync(bb);
        

        return Guid.NewGuid();

    }

    public async Task ChangePasswordAsync(BaseEntity entity)
    {
        return;
    }
}