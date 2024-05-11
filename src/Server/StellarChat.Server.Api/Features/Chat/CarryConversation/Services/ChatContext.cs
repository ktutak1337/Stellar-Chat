namespace StellarChat.Server.Api.Features.Chat.CarryConversation.Services;

internal class ChatContext : IChatContext
{
    private readonly IHubContext<MessageBrokerHub> _messageBrokerHubContext;

    public ChatContext(IHubContext<MessageBrokerHub> messageBrokerHubContext)
    {
        _messageBrokerHubContext = messageBrokerHubContext;
    }

    public async Task UpdateMessageOnClient(ChatMessage chatMessage, string message, CancellationToken cancellationToken = default)
        => await _messageBrokerHubContext.Clients.Group(chatMessage.ChatId.ToString()).SendAsync("ReceiveMessageUpdate", message, cancellationToken);
}
