using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
    
namespace InTouch.Application;

public static class ConfigureService
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        return services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        //.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)));
    }
}