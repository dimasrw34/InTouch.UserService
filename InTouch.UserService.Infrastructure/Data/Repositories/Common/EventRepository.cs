using System.Data;
using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public class EventRepository(IDbContext context) 
    : BaseWriteOnlyRepository(context),
        IEventStoreRepository
{
  public async Task StoreAsync(EventStore? eventStore)
    {
        var _sql = @"CALL public.create_event(@eventdid_, @datastamp_, @messagetype_, @aggregateid_)";

        var _param = new 
        {
            eventdid_= eventStore.Id,
            datastamp_ = eventStore.Data,
            messagetype_ = eventStore.MessageType,
            aggregateid_ = eventStore.AggregateID
        };
        
        await ExecuteAsync(_sql, _param);
    }
}