namespace StellarChat.Server.Api;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSharedInfrastructure();

        builder.Services.AddSignalR();
        builder.Services.TryAddSingleton(TimeProvider.System);
        builder.Services.AddMappings();
        builder.Services
            .AddScoped<IChatMessageRepository, ChatMessageRepository>()
            .AddScoped<IChatSessionRepository, ChatSessionRepository>()
            .AddScoped<IAssistantRepository, AssistantRepository>()
            .AddMongoRepository<ChatMessageDocument, Guid>("messages")
            .AddMongoRepository<ChatSessionDocument, Guid>("chat-sessions")
            .AddMongoRepository<AssistantDocument, Guid>("assistants");

        builder.Services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.MapHub<MessageBrokerHub>("/hub");
        app.UseSharedInfrastructure();

        return app;
    }

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
