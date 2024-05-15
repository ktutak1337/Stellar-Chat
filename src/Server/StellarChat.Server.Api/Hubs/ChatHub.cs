namespace StellarChat.Server.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger) 
        => _logger = logger;

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client with connection ID: {Context.ConnectionId} connected.");

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client with connection ID {Context.ConnectionId} disconnected.");

        return base.OnDisconnectedAsync(exception);
    }

    public async Task ReceiveChatMessageChunk(string message)
    {
        await Clients.All.ReceiveChatMessageChunk(message);
        _logger.LogInformation($"Message sent to all clients: {message}");
    }
}
