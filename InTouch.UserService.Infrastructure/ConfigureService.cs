using System;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

using InTouch.Infrastructure.Data;
using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddTransient<IDbConnectionFactory> (options =>
            {
                var builder = new NpgsqlConnectionStringBuilder(
                    "Host=192.168.1.116;Database=postgres;Username=root;Password=example;Persist Security Info=True;Application Name=userservice;Enlist=true");
                return new DbConnectionFactory(() =>
                {
                    var conn = new NpgsqlConnection(builder.ConnectionString);
                    conn.Open();
                    return conn;
                });
            })
            .AddScoped<IDbContext, DbContext>();
    }
    public static IServiceCollection AddWriteOnlyRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserWriteOnlyRepository<User, Guid>, UserWriteOnlyRepository>()
            .AddScoped<IEventStoreRepository,UserEventRepository>();
    }
}