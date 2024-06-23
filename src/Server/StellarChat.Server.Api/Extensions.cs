using Microsoft.Extensions.FileProviders;
using Microsoft.KernelMemory;
using Microsoft.SemanticKernel;
using StellarChat.Server.Api.DAL.Mongo.Repositories.Actions;
using StellarChat.Server.Api.DAL.Mongo.Repositories.Settings;
using StellarChat.Server.Api.DAL.Mongo.Seeders;
using StellarChat.Server.Api.Features.Actions.Webhooks.Services;
using StellarChat.Server.Api.Features.Chat.CarryConversation;
using StellarChat.Server.Api.Features.Models.Providers;
using StellarChat.Server.Api.Options;

namespace StellarChat.Server.Api;

internal static class Extensions
{
    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        host.ConfigureAppConfiguration((builderContext, configBuilder) =>
        {
            configBuilder.AddJsonFile(
                path: "appsettings.json",
                optional: false,
                reloadOnChange: true);

            configBuilder.AddJsonFile(
                path: $"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: true);

            configBuilder.AddEnvironmentVariables();

            configBuilder.AddUserSecrets(
                assembly: Assembly.GetExecutingAssembly(),
                optional: true,
                reloadOnChange: true);
        });

        return host;
    }

    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.TryAddSingleton(TimeProvider.System);
        builder.Services.AddScoped<IAppSettingsSeeder, AppSettingsSeeder>();
        builder.Services.AddScoped<IAssistantsSeeder, AssistantsSeeder>();
        builder.Services.AddScoped<IActionsSeeder, ActionsSeeder>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.AddSharedInfrastructure();

        builder.Services.AddMemoryCache();
        builder.Services.AddHttpClient("Webhooks");
        builder.Services.AddSignalR();
        builder.Services.AddMappings();

        builder.Services
            .AddScoped<IHttpClientService, HttpClientService>()
            .AddScoped<IChatMessageRepository, ChatMessageRepository>()
            .AddScoped<IChatSessionRepository, ChatSessionRepository>()
            .AddScoped<IAssistantRepository, AssistantRepository>()
            .AddScoped<INativeActionRepository, NativeActionRepository>()
            .AddScoped<ISettingsRepository, SettingsRepository>()
            .AddScoped<IDefaultAssistantService, DefaultAssistantService>()
            .AddScoped<IChatContext, ChatContext>()
            .AddScoped<IModelsProvider, OpenAiModelsProvider>()
            .AddMongoRepository<ChatMessageDocument, Guid>("messages")
            .AddMongoRepository<ChatSessionDocument, Guid>("chat-sessions")
            .AddMongoRepository<AssistantDocument, Guid>("assistants")
            .AddMongoRepository<NativeActionDocument, Guid>("actions")
            .AddMongoRepository<AppSettingsDocument, Guid>("settings");

        builder.Services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        builder.Services.AddSemanticKernel(builder.Configuration);
        builder.Services.AddKernelMemory(builder.Configuration);
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMongo();

        var basePath = Path.Combine(app.Environment.ContentRootPath, "_data");

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(basePath),
            RequestPath = "/files"
        });
        app.MapHub<ChatHub>("/hub");
        app.UseSharedInfrastructure();

        return app;
    }

    public static WebApplication UseMongo(this WebApplication app)
    {
        app.ConfigureChatMessageIndexes()
           .ConfigureChatSessionIndexes()
           .ConfigureAssistantIndexes()
           .ConfigureNativeActionIndexes();

        return app;
    }

    public static WebApplication ConfigureChatMessageIndexes(this WebApplication app)
    {
        var chatMessages = app.Services.GetRequiredService<IMongoRepository<ChatMessageDocument, Guid>>().Collection;
        var chatMessageBuilder = Builders<ChatMessageDocument>.IndexKeys;

        chatMessages.Indexes.CreateMany(
        [
            new CreateIndexModel<ChatMessageDocument>(chatMessageBuilder.Text(i => i.Content),
                new CreateIndexOptions
                {
                    Background = true
                }),
            new CreateIndexModel<ChatMessageDocument>(chatMessageBuilder.Ascending(i => i.Id),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                }),
            new CreateIndexModel<ChatMessageDocument>(chatMessageBuilder.Ascending(i => i.ChatId),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                })
        ]);

        return app;
    }

    public static WebApplication ConfigureChatSessionIndexes(this WebApplication app)
    {
        var chatSessions = app.Services.GetRequiredService<IMongoRepository<ChatSessionDocument, Guid>>().Collection;
        var chatSessionBuilder = Builders<ChatSessionDocument>.IndexKeys;

        chatSessions.Indexes.CreateMany(
        [
            new CreateIndexModel<ChatSessionDocument>(chatSessionBuilder.Ascending(i => i.Id),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                }),
            new CreateIndexModel<ChatSessionDocument>(chatSessionBuilder.Ascending(i => i.AssistantId),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                })
        ]);

        return app;
    }

    public static WebApplication ConfigureAssistantIndexes(this WebApplication app)
    {
        var assistants = app.Services.GetRequiredService<IMongoRepository<AssistantDocument, Guid>>().Collection;
        var assistantBuilder = Builders<AssistantDocument>.IndexKeys;

        assistants.Indexes.CreateMany(
        [
            new CreateIndexModel<AssistantDocument>(assistantBuilder.Ascending(i => i.Id),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                }),
            new CreateIndexModel<AssistantDocument>(assistantBuilder.Ascending(i => i.Name),
                new CreateIndexOptions
                {
                    Background = true
                })
        ]);

        return app;
    }

    public static WebApplication ConfigureNativeActionIndexes(this WebApplication app)
    {
        var nativeActions = app.Services.GetRequiredService<IMongoRepository<NativeActionDocument, Guid>>().Collection;
        var nativeActionBuilder = Builders<NativeActionDocument>.IndexKeys;

        nativeActions.Indexes.CreateMany(
        [
            new CreateIndexModel<NativeActionDocument>(nativeActionBuilder.Ascending(i => i.Id),
                new CreateIndexOptions
                {
                    Unique = true,
                    Background = true
                }),
            new CreateIndexModel<NativeActionDocument>(nativeActionBuilder.Ascending(i => i.Name),
                new CreateIndexOptions
                {
                    Background = true
                })
        ]);

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
        services.AddSingleton(options);

        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: options.TEXT_MODEL,
                apiKey: options.API_KEY)
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
                APIKey = openAiOptions.API_KEY,
                TextModel = openAiOptions.TEXT_MODEL,
                EmbeddingModel = openAiOptions.EMBEDDING_MODEL
            })
            .WithQdrantMemoryDb(new QdrantConfig
            {
                Endpoint = qdrantOptions.ENDPOINT,
            })
            .Build();

        services.AddSingleton(memory);

        return services;
    }
}
