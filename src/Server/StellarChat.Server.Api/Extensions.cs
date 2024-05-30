using Microsoft.KernelMemory;
using Microsoft.SemanticKernel;
using StellarChat.Server.Api.DAL.Mongo.Repositories.Actions;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;
using StellarChat.Server.Api.Features.Chat.CarryConversation;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api;

internal static class Extensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.AddSharedInfrastructure();

        builder.Services.AddHttpClient("Webhooks");
        builder.Services.AddSignalR();
        builder.Services.TryAddSingleton(TimeProvider.System);
        builder.Services.AddMappings();

        builder.Services
            .AddScoped<IHttpClientService, HttpClientService>()
            .AddScoped<IChatMessageRepository, ChatMessageRepository>()
            .AddScoped<IChatSessionRepository, ChatSessionRepository>()
            .AddScoped<IAssistantRepository, AssistantRepository>()
            .AddScoped<INativeActionRepository, NativeActionRepository>()
            .AddScoped<IDefaultAssistantService, DefaultAssistantService>()
            .AddScoped<IChatContext, ChatContext>()
            .AddMongoRepository<ChatMessageDocument, Guid>("messages")
            .AddMongoRepository<ChatSessionDocument, Guid>("chat-sessions")
            .AddMongoRepository<AssistantDocument, Guid>("assistants")
            .AddMongoRepository<NativeActionDocument, Guid>("actions");

        builder.Services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        builder.Services.AddSemanticKernel(builder.Configuration);
        builder.Services.AddKernelMemory(builder.Configuration);
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.MapHub<ChatHub>("/hub");
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

    public static IServiceCollection AddSemanticKernel(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(OpenAiOptions.Key);
        var options = section.BindOptions<OpenAiOptions>();
        services.Configure<OpenAiOptions>(section);

        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: options.TextModel,
                apiKey: options.ApiKey)
            .Build();

        services.AddSingleton(kernel);

        using var serviceProvider = services.BuildServiceProvider();
        
        var chatContext = serviceProvider.GetRequiredService<IChatContext>();
        var clock = serviceProvider.GetRequiredService<TimeProvider>();

        kernel.ImportPluginFromObject(new ChatPlugin(chatContext, clock), nameof(ChatPlugin));

        return services;
    }

    public static IServiceCollection AddKernelMemory(this IServiceCollection services, IConfiguration configuration)
    {
        var openAiSection = configuration.GetSection(OpenAiOptions.Key);
        var openAiOptions = openAiSection.BindOptions<OpenAiOptions>();

        var qdrantSection = configuration.GetSection(QdrantOptions.Key);
        var qdrantOptions = qdrantSection.BindOptions<QdrantOptions>();
        services.Configure<QdrantOptions>(qdrantSection);

        var memory = new KernelMemoryBuilder()
            .WithOpenAI(new OpenAIConfig
            {
                APIKey = openAiOptions.ApiKey,
                OrgId = openAiOptions.OrganizationId,
                TextModel = openAiOptions.TextModel,
                EmbeddingModel = openAiOptions.EmbeddingModel
            })
            .WithQdrantMemoryDb(new QdrantConfig
            {
                Endpoint = qdrantOptions.Endpoint,
            })
            .Build();

        services.AddSingleton(memory);

        return services;
    }
}
