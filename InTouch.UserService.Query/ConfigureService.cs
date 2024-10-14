using System.Reflection;
using AutoMapper;
using InTouch.UserService.Query.Data;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace InTouch.UserService.Query;

public static class ConfigureService
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddMediatR(cfg => { 
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.NotificationPublisher = new TaskWhenAllPublisher();
            })
            .AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(assembly))));
        //.AddValidatorsFromAssembly(assembly);
        
        
    }
    public static IServiceCollection AddReadDbContext(this IServiceCollection services)
    {
        services
            .AddSingleton<ISynchronizedDb, NoSqlDbContext>()
            .AddSingleton<IReadDbContext, NoSqlDbContext>()
            .AddSingleton<NoSqlDbContext>();

        ConfigureMongoDb();

        return services;
    }
   
    private static void ConfigureMongoDb()
    {
        try
        {
            // Step 1: Configure the serializer for Guid type.
            BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

            // Step 2: Configure the conventions to be applied to all mappings.
            // REF: https://mongodb.github.io/mongo-csharp-driver/2.0/reference/bson/mapping/conventions/
            ConventionRegistry.Register("Conventions",
                new ConventionPack
                {
                    new CamelCaseElementNameConvention(), // Convert element names to camel case
                    new EnumRepresentationConvention(BsonType.String), // Serialize enums as strings
                    new IgnoreExtraElementsConvention(true), // Ignore extra elements when deserializing
                    new IgnoreIfNullConvention(true) // Ignore null values when serializing
                }, _ => true);

            // Step 3: Register the mappings configurations.
            // It is recommended to register all mappings before initializing the connection with MongoDb
            // REF: https://mongodb.github.io/mongo-csharp-driver/2.0/reference/bson/mapping/
            new UserMap().Configure(); // Configuration for Customer class
        }
        catch
        {
            // Ignore
        }
    }
}