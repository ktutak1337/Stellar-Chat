using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory;
using Microsoft.SemanticKernel;

namespace StellarChat.Shared.Infrastructure.Semantic;

public static class Extensions
{
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
