using StellarChat.Shared.Contracts.Assistants;

namespace StellarChat.Client.Web.State;

public class ChatState
{
    public bool IsFullWidthText { get; private set; }
    public event Action? FullWidthTextChanged;

    public void SetFullWidthText(bool isFullWidth)
    {
        IsFullWidthText = isFullWidth;
        FullWidthTextChanged?.Invoke();
    }

    public Guid ChatId { get; set; }
    public event Action? ChatIdChanged;

    public void SetChatId(Guid chatId)
    {
        ChatId = chatId;
        ChatIdChanged?.Invoke();
    }

    public bool IsNewChatPending { get; set; }
    public event Action? IsNewChatPendingChanged;

    public void SetIsNewChatPending(bool value)
    {
        IsNewChatPending = value;
        IsNewChatPendingChanged?.Invoke();
    }

    public string SelectedModel { get; set; } = string.Empty;
    public event Action? SelectedModelChanged;

    public void SetSelectedModel(string selectedModel)
    {
        SelectedModel = selectedModel;
        SelectedModelChanged?.Invoke();
    }

    public string ServiceId { get; set; } = string.Empty;
    public event Action? ServiceIdChanged;

    public void SetServiceId(string serviceId)
    {
        ServiceId = serviceId;
        ServiceIdChanged?.Invoke();
    }

    public Guid ActionId { get; set; }
    public event Action? ActionIdChanged;

    public void SetActionId(Guid actionId)
    {
        ActionId = actionId;
        ActionIdChanged?.Invoke();
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

    public AssistantResponse? SelectedAssistant { get; set; }
    public event Action? SelectedAssistantChanged;

    public void SetSelectedAssistant(AssistantResponse assistant)
    {
        SelectedAssistant = assistant;
        SelectedAssistantChanged?.Invoke();
    }

    public AssistantResponse? AssignedAssistant { get; set; }
    public event Action? AssignedAssistantChanged;

    public void SetAssignedAssistant(AssistantResponse assistant)
    {
        AssignedAssistant = assistant;
        AssignedAssistantChanged?.Invoke();
    }

    public event Action? AssistantUpdated;

    public void NotifyAssistantUpdated()
    {
        AssistantUpdated?.Invoke();
    }

    public event Action? ActionsRefreshed;

    public void NotifyActionsRefreshed()
    {
        ActionsRefreshed?.Invoke();
    }
}
