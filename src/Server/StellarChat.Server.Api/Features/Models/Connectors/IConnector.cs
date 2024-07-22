using Microsoft.SemanticKernel;

namespace StellarChat.Server.Api.Features.Models.Connectors;

internal interface IConnector
{
    string ProviderName { get; }
    Kernel Kernel { get; }
    Kernel CreateKernel(string modelId);
}
