namespace StellarChat.Server.Api.Domain.Actions.Exceptions;

internal class InvalidUrlAddressException : StellarChatException
{
    public string Url { get; }

    public InvalidUrlAddressException(string url)
        : base(
            message: $"The provided URL '{url}' is invalid. Please ensure it is a well-formed absolute URL.",
            userMessage: $"The URL '{url}' you entered is not valid. Please check the URL and try again.")
        => Url = url;
}
