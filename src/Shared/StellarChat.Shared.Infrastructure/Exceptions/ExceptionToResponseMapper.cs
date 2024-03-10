using StellarChat.Shared.Abstractions.Exceptions;
using System.Collections.Concurrent;
using System.Net;

namespace StellarChat.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private readonly ExceptionResponse _defaultResponse =
        new(new ErrorsResponse(new Error(
            Code: "error", 
            Message: "There was an error.", 
            UserMessage: "Internal server error occurred.")), 
        HttpStatusCode.InternalServerError);

    private readonly ExceptionResponse _communicationResponse = new(
        new ErrorsResponse(new Error(
            Code: "internal_service_http_communication",
            Message: "There was an internal HTTP service communication error.",
            UserMessage: "Internal communication error occurred. Please try again later.")),
        HttpStatusCode.InternalServerError);

    private readonly ConcurrentDictionary<Type, string> _errorCodes = new();

    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            StellarChatException ex => CreateStellarChatErrorResponse(ex, HttpStatusCode.BadRequest),
            HttpRequestException _ => _communicationResponse,
            _ => _defaultResponse
        };

    private record Error(string Code, string Message, string UserMessage);

    private record ErrorsResponse(params Error[] Errors);

    private string GetErrorCode(Exception exception)
    {
        var type = exception.GetType();
        return _errorCodes.GetOrAdd(type, type.Name.ToSnakeCase().Replace("_exception", string.Empty));
    }

    private ExceptionResponse CreateStellarChatErrorResponse(StellarChatException exception, HttpStatusCode statusCode)
    {
        var error = new Error(GetErrorCode(exception), exception.Message, exception.UserMessage);
        var errorsResponse = new ErrorsResponse(error);
        return new ExceptionResponse(errorsResponse, statusCode);
    }
}
