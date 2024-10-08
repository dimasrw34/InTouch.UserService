using InTouch.UserService.Core;

namespace InTouch.Infrastructure.Data;

public class EventRepository : BasePostgreRepository, IEventStoreRepository
{
  public async Task StoreAsync(EventStore? eventStore)
    {
        Console.WriteLine("Save EventStore to Repo with    " + "\n"
                                                             + "  AggregateID: " + eventStore.AggregateID.ToString()+ "\n"
                                                             + "  MessageType " + eventStore.MessageType.ToString()+ "\n"
                                                             + "  Data JSON:  " + eventStore.Data+ "\n"
                                                             + "  row ID: " + eventStore.Id+ "\n"
                                                             + " date: " + eventStore.OccuredOn);
    }
}