using AutoMapper;
using InTouch.UserService.Domain;
using MediatR;

namespace InTouch.UserService.Query;

public class UserEventHandler (
    ISynchronizedDb synchronizedDb,
    IMapper mapper):
    INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userQueryModel = mapper.Map<UserQueryModel>(notification);
        await synchronizedDb.UpsertAsync(userQueryModel, filter => filter.Id == userQueryModel.Id);
        
        Console.WriteLine(notification.OccuredOn + " Save to MongoDB ID  " + notification.Id + "   " + notification.Email.ToString());
    }
}