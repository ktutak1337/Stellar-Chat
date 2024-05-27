namespace StellarChat.Client.Web.State;

public class ChatState
{
    public Guid ChatId { get; set; }
    public event Action? ChatIdChanged;

    public void SetChatId(Guid chatId)
    {
        ChatId = chatId;
        ChatIdChanged?.Invoke();
    }

    public void CreateNewChat()
    {
        ChatId = Guid.Empty;
        ChatIdChanged?.Invoke();
    }
}
