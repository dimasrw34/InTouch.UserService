
using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public class EventRepository : BasePostgreRepository, IEventStoreRepository
{
  public async Task StoreAsync(EventStore? eventStore)
    {
        Console.WriteLine("Сохраняем  EventStore (события) to PostreSQL with    " + "\n"
                                                             + "  AggregateID: " + eventStore.AggregateID.ToString()+ "\n"
                                                             + "  MessageType " + eventStore.MessageType.ToString()+ "\n"
                                                             + "  Data JSON:  " + eventStore.Data+ "\n"
                                                             + "  row ID: " + eventStore.Id+ "\n"
                                                             + " date: " + eventStore.OccuredOn);
        var _sql = @"CALL public.create_event(@eventdid_, @datastamp_, @messagetype_, @aggregateid_)";

        var _param = new 
        {
            eventdid_= eventStore.Id,
            datastamp_ = eventStore.Data,
            messagetype_ = eventStore.MessageType,
            aggregateid_ = eventStore.AggregateID
        };
        //сохраняем в базу
        await ExecuteAsync(_sql, _param);
    }
}