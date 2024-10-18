using InTouch.Infrastructure.Data;
using InTouch.UserService.Core;
using InTouch.UserService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace InTouch.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      return services
          .AddScoped<IDbContext,PostgreDbContext>()
          .AddScoped<UnitOfWork>();
    }
    public static IServiceCollection AddWriteOnlyRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserWriteOnlyRepository<User, Guid>,UserWriteOnlyRepository>()
            .AddScoped<IEventStoreRepository,EventRepository>();
    }
}