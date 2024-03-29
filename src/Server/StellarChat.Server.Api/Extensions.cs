using Mapster;
using MapsterMapper;
using System.Reflection;
using StellarChat.Shared.Infrastructure;
using StellarChat.Shared.Infrastructure.DAL.Mongo;
using StellarChat.Server.Api.DAL.Mongo.Documents.Chat;
using StellarChat.Server.Api.DAL.Mongo.Repositories.Chat;
using StellarChat.Server.Api.Domain.Chat.Repositories;

namespace StellarChat.Server.Api;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSharedInfrastructure();

        builder.Services.AddMappings();
        builder.Services
            .AddScoped<IChatMessageRepository, ChatMessageRepository>()
            .AddScoped<IChatSessionRepository, ChatSessionRepository>()
            .AddMongoRepository<ChatMessageDocument, Guid>("messages")
            .AddMongoRepository<ChatSessionDocument, Guid>("chat-sessions");
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
        => app.UseSharedInfrastructure();

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
