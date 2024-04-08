using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
}
