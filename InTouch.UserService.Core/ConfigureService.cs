using System.Reflection;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace InTouch.UserService.Core;

public static class ConfigureService
{
    public static IServiceCollection AddResponseMediatr(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.NotificationPublisher = new TaskWhenAllPublisher();
        });
    }
}