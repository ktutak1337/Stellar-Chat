namespace StellarChat.Server.Api.Features.Models.Connectors;

internal class ConnectorStrategy(IEnumerable<IConnector> connectors) : IConnectorStrategy
{
    private readonly IEnumerable<IConnector> _connectors = connectors;

    IConnector IConnectorStrategy.SelectConnector(string serviceId)
    {
        var connector = _connectors.SingleOrDefault(c => c.ProviderName.Equals(serviceId, StringComparison.OrdinalIgnoreCase));

        if (connector is null)
        {
            throw new InvalidOperationException($"Unsupported service type: {serviceId}");
        }

        return connector;
    }
}
