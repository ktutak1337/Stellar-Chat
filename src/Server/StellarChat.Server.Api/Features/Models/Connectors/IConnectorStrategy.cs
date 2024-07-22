namespace StellarChat.Server.Api.Features.Models.Connectors;

internal interface IConnectorStrategy
{
    IConnector SelectConnector(string serviceId);
}
