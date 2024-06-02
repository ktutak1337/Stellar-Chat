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

    public string UserName { get; set; } = string.Empty;
    public event Action? UserNameChanged;

    public void SetUserName(string userName)
    {
        UserName = userName;
        UserNameChanged?.Invoke();
    }

    public string UserAvatar { get; set; } = string.Empty;
    public event Action? UserAvatarChanged;

    public void SetUserAvatar(string userAvatar)
    {
        UserAvatar = userAvatar;
        UserAvatarChanged?.Invoke();
    }
}
