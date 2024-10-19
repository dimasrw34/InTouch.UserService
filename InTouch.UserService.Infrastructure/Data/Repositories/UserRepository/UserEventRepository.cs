using System.Threading.Tasks;
using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public class UserEventRepository(IDbContext context) 
    : BaseWriteOnlyRepository(context),
        IEventStoreRepository
{
  public async Task StoreAsync(EventStore? eventStore)
    {
        await ExecuteAsync
            (@"CALL public.create_event(
                        @eventdid_,
                        @datastamp_,
                        @messagetype_,
                        @aggregateid_)",
            new { eventdid_= eventStore.Id,
                        datastamp_ = eventStore.Data,
                        messagetype_ = eventStore.MessageType,
                        aggregateid_ = eventStore.AggregateID
                        });
    }
}