namespace StellarChat.Server.Api.DAL.Mongo.Repositories.Chat;

internal class ChatMessageRepository : IChatMessageRepository
{
    private readonly IMongoRepository<ChatMessageDocument, Guid> _repository;

    public ChatMessageRepository(IMongoRepository<ChatMessageDocument, Guid> repository)
        => _repository = repository;

    public async ValueTask<ChatMessage?> GetAsync(Guid messageId)
        => (await _repository.GetAsync(document => document.Id == messageId)).Adapt<ChatMessage>();

    public async ValueTask<IEnumerable<ChatMessage>> BrowseMessagesAsync()
        => (await _repository.FindAsync(_ => true)).Adapt<IEnumerable<ChatMessage>>();

    public async ValueTask<IEnumerable<ChatMessage>> FindMessagesByChatIdAsync(Guid chatId)
        => (await _repository.FindAsync(document => document.ChatId == chatId)).Adapt<IEnumerable<ChatMessage>>();

    public async ValueTask<ChatMessage?> FindLatestMessageByChatIdAsync(Guid chatId)
    {
        var messages = await FindMessagesByChatIdAsync(chatId);
        var theLatestMessage = messages
            .MaxBy(e => e.Timestamp)
            .Adapt<ChatMessage>();

        return theLatestMessage;
    }

    public async ValueTask AddAsync(ChatMessage message)
        => await _repository.AddAsync(message.Adapt<ChatMessageDocument>());

    public async ValueTask UpdateAsync(ChatMessage message)
        => await _repository.UpdateAsync(message.Adapt<ChatMessageDocument>());

    public async ValueTask DeleteAsync(Guid messageId)
        => await _repository.DeleteAsync(document => document.Id == messageId);

    public async ValueTask<bool> ExistsAsync(Guid messageId)
        => await _repository.ExistsAsync(document => document.Id == messageId);
}
