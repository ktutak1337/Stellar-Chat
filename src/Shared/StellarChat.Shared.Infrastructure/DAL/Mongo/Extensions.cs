using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using StellarChat.Shared.Infrastructure.DAL.Mongo.Factories;
using StellarChat.Shared.Infrastructure.DAL.Mongo.Seeders;
using Microsoft.Extensions.DependencyInjection;
using StellarChat.Shared.Infrastructure.DAL.Mongo.Repositories;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration, Type? seederType = null)
    {
        var section = configuration.GetSection("mongo");
        var options = section.BindOptions<MongoOptions>();
        services.Configure<MongoOptions>(section);

        if (!section.Exists())
        {
            return services;
        }

        var mongoClient = new MongoClient(options.CONNECTION_STRING);
        var database = mongoClient.GetDatabase(options.DATABASE);
        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddSingleton(database);

        services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

        if (seederType is null)
        {
            services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();

            using var scope = services.BuildServiceProvider().CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IMongoDbSeeder>();
            seeder.SeedAsync(database).GetAwaiter().GetResult();
        }
        else
        {
            services.AddTransient(typeof(IMongoDbSeeder), seederType);
        }

        RegisterConventions();

        return services;
    }

    public static IServiceCollection AddMongoRepository<TEntity, TIdentifiable>(this IServiceCollection services, string collectionName)
        where TEntity : IIdentifiable<TIdentifiable>
    {
        services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
        });

        return services;
    }

    private static void RegisterConventions()
    {
        BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
        BsonSerializer.RegisterSerializer(typeof(decimal?),
            new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
        ConventionRegistry.Register("mongo-conventions", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
        }, _ => true);
    }
}
