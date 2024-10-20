using AutoMapper;
using InTouch.UserService.Domain;
using InTouch.UserService.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InTouch.UserService.Query;

public class UserEventHandler (
    ISynchronizedDb synchronizedDb,
    ILogger<UserEventHandler> logger,
    IMapper mapper):
    INotificationHandler<UserCreatedEvent>,
    INotificationHandler<UserUpdatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userQueryModel = mapper.Map<UserQueryModel>(notification);
        await synchronizedDb.UpsertAsync(userQueryModel, filter => filter.Id == userQueryModel.Id);
    }

    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Медиатор сработал");
        var userQueryModel = mapper.Map<UserQueryModel>(notification);
        await synchronizedDb.UpsertAsync(userQueryModel, filter => filter.Id == userQueryModel.Id);
    }
    
    private void LogEvent<TEvent>(TEvent @event) where TEvent : UserBaseEvent =>
        logger.LogInformation("----- Triggering the event {EventName}, model: {EventModel}", typeof(TEvent).Name, @event.ToJson());
}