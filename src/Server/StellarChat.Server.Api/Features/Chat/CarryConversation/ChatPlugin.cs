using Microsoft.SemanticKernel;
using StellarChat.Server.Api.Features.Chat.CarryConversation.Services;
using System.ComponentModel;

namespace StellarChat.Server.Api.Features.Chat.CarryConversation;

internal class ChatPlugin
{
    private readonly IChatContext _chatContext;

    public ChatPlugin(IChatContext chatContext)
    {
        _chatContext = chatContext;
    }

    [KernelFunction, Description("Get chat response")]
    public async Task<KernelArguments> ChatAsync(
        [Description("The new message used as input")] string message, 
        [Description("Type of the message")] string messageType, 
        [Description("Unique identifier for the chat")] string chatId, 
        KernelArguments context)
    {
        var chatContextArguments = new KernelArguments(context);

        return chatContextArguments;
    }
}
