using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InTouch.UserService.Query;

public static class ConfigureService
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        //.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(assembly))))
        //.AddValidatorsFromAssembly(assembly);
    }

}