namespace StellarChat.Server.Api.Features.Models.BrowseAvailableModels;

internal sealed record BrowseAvailableModels : IQuery<IEnumerable<AvailableModelsResponse>> 
{
    public string? Provider { get; set; }
    public string? Filter { get; set; }
}
