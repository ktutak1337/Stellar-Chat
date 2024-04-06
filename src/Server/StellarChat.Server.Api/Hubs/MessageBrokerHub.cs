using Microsoft.AspNetCore.SignalR;

namespace StellarChat.Server.Api.Hubs;

internal sealed class MessageBrokerHub : Hub
{
    private const string ReceiveMessageClientCall = "ReceiveMessage";
    private readonly ILogger<MessageBrokerHub> _logger;

    public MessageBrokerHub(ILogger<MessageBrokerHub> logger)
        => _logger = logger;

    public async Task AddClientToGroupAsync(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        _logger.LogInformation($"Client has been added to group with chatID: {chatId}");
    }

    public async Task SendMessageAsync(string chatId, object message)
    {
        await Clients.Group(chatId).SendAsync(ReceiveMessageClientCall, chatId, message);
        _logger.LogInformation($"Message has been sent to group with chatID: {chatId}");
    }
}
