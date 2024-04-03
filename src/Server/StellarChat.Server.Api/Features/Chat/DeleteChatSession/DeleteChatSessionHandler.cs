using Mediator;
using StellarChat.Server.Api.DAL.Mongo.Exceptions.Chat;
using StellarChat.Server.Api.Domain.Chat.Repositories;

namespace StellarChat.Server.Api.Features.Chat.DeleteChatSession;

internal sealed class DeleteChatSessionHandler : ICommandHandler<DeleteChatSession>
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly ILogger<DeleteChatSessionHandler> _logger;

    public DeleteChatSessionHandler(IChatMessageRepository chatMessageRepository, IChatSessionRepository chatSessionRepository, 
        ILogger<DeleteChatSessionHandler> logger)
    {
        _chatMessageRepository = chatMessageRepository;
        _chatSessionRepository = chatSessionRepository;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(DeleteChatSession command, CancellationToken cancellationToken)
    {
        if (!await _chatSessionRepository.ExistsAsync(command.ChatId))
        {
            throw new ChatSessionNotFoundException(command.ChatId);
        }

        await DeleteMessagesForChatSessionAsync(command.ChatId, cancellationToken);
        await _chatSessionRepository.DeleteAsync(command.ChatId);

        _logger.LogInformation($"Chat session with ID: '{command.ChatId}' has been deleted.");
        
        return Unit.Value;
    }

    private async ValueTask DeleteMessagesForChatSessionAsync(Guid chatId, CancellationToken cancellationToken = default)
    {
        var cleanupTasks = new List<Task>();

        var messages = await _chatMessageRepository.FindMessagesByChatIdAsync(chatId);

        foreach (var message in messages)
        {
            cleanupTasks.Add(_chatMessageRepository.DeleteAsync(message.Id).AsTask());
        }

        var aggregationTask = Task.WhenAll(cleanupTasks);

        try
        {
            // INFO: Await the completion of all tasks in parallel
            await aggregationTask;
        }
        catch (Exception exception)
        {
            // INFO: Handle any exceptions that occurred during the tasks
            if (aggregationTask?.Exception?.InnerExceptions is not null && aggregationTask.Exception.InnerExceptions.Count != 0)
            {
                foreach (var innerException in aggregationTask.Exception.InnerExceptions)
                {
                    _logger.LogInformation($"Failed to delete a entity of chat '{chatId}': {innerException.Message}");
                }

                throw aggregationTask.Exception;
            }

            throw new AggregateException($"Resource deletion failed for chat {chatId}.", exception);
        }

        _logger.LogInformation($"All messages for chat session with ID: '{chatId}' has been deleted.");
    }
}
