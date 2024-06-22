using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;
using ChatHistory = Microsoft.SemanticKernel.ChatCompletion.ChatHistory;

namespace StellarChat.Server.Api.Features.Chat.CreateChatSession;

internal class CreateChatSessionHandler : ICommandHandler<CreateChatSession>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly Kernel _kernel;
    private readonly TimeProvider _clock;
    private readonly IHubContext<ChatHub, IChatHub> _hubContext;
    private readonly ILogger<CreateChatSessionHandler> _logger;

    public CreateChatSessionHandler(
        IChatSessionRepository chatSessionRepository,
        Kernel kernel,
        TimeProvider clock,
        IHubContext<ChatHub, IChatHub> hubContext,
        ILogger<CreateChatSessionHandler> logger)
    {
        _chatSessionRepository = chatSessionRepository;
        _kernel = kernel;
        _clock = clock;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async ValueTask<Unit> Handle(CreateChatSession command, CancellationToken cancellationToken)
    {
        if (await _chatSessionRepository.ExistsAsync(command.ChatId))
        {
            throw new ChatSessionAlreadyExistsException(command.ChatId);
        }

        var now = _clock.GetLocalNow();

        // TODO: Retrieve activePlugins and metaprompt from settings
        var activePlugins = new HashSet<string>();

        var chatSession = ChatSession.Create(command.ChatId, command.AssistantId, command.Title, metaprompt: "", activePlugins, createdAt: now, updatedAt: now);

        await _chatSessionRepository.AddAsync(chatSession);
        _logger.LogInformation($"Chat session with ID: '{chatSession.Id}' has been created.");

        _ = Task.Run(async () =>
        {
            await GenerateChatTitle(chatSession, command.Message);
        });

        return Unit.Value;
    }

    private async Task GenerateChatTitle(ChatSession chatSession, string message)
    {
        await Task.Delay(1000);
        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        var reply = new StringBuilder();

        string prompt = "Generate a concise chat title based on the user’s first message. The title should be a short, one-sentence summary in English.";

        var chatHistory = new ChatHistory(prompt);
        chatHistory.AddUserMessage(message);

        await foreach (var contentPiece in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, new PromptExecutionSettings() { ModelId = "gpt-3.5-trubo" }))
        {
            if (contentPiece.Content is { Length: > 0 })
            {
                reply.Append(contentPiece.Content);
                Console.WriteLine(contentPiece.Content);
                await _hubContext.Clients.All.ReceiveUpdateChatTitle(chatSession.Id, contentPiece.Content);
            }
        }

        chatSession.Title = reply.ToString();
        reply.Clear();

        await _chatSessionRepository.UpdateAsync(chatSession);
    }
}
