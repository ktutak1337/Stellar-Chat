namespace StellarChat.Server.Api.Features.Actions.ExecuteNativeAction;

internal static class RemoteActionMessagesConstant
{
    public const string PreparingPayload = "🔄 Preparing payload for remote action.";
    public const string ProcessingStatus = "⏳ Your action is currently being processed. Please wait a moment.";
    public const string FailedProcessingStatus = "❌ Your action failed to process. Please check the logs for more details.";
    public const string NoContentStatus = "⚠️ The response contained no content. Status code: {0}.";
}
