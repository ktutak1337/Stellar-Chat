namespace StellarChat.Server.Api.Domain.Actions.Exceptions;

internal class InvalidHttpMethodException : StellarChatException
{
    public string HttpMethod { get; }

    public InvalidHttpMethodException(string httpMethod, List<string> allowedHttpMethods)
        : base(
            message: $"Invalid HTTP method: '{httpMethod}'. Allowed methods: {FormatAllowedMethods(allowedHttpMethods)}",
            userMessage: $"The HTTP method '{httpMethod}' you provided is not supported. Please use one of the following methods: {FormatAllowedMethods(allowedHttpMethods)}.")
        => HttpMethod = httpMethod;

    private static string FormatAllowedMethods(List<string> allowedHttpMethods) 
        => string.Join(", ", allowedHttpMethods ?? []);
}
