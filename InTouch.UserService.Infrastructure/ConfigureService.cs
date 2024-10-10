using InTouch.Infrastructure.Data;
using InTouch.UserService.Core;
using Microsoft.Extensions.DependencyInjection;

namespace InTouch.Infrastructure;

public static class ConfigureService
{
    

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      return services
        .AddScoped<IUnitOfWork, UnitOfWork>();
       
    }
    public static IServiceCollection AddWriteOnlyRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
       
    }


}