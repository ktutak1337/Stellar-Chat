using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StellarChat.Shared.Abstractions.Exceptions;
using System.Net;

namespace StellarChat.Shared.Infrastructure.Exceptions;

internal sealed class ErrorExceptionHandler : IExceptionHandler
{
    private readonly IExceptionToResponseMapper _mapper;
    private readonly ILogger<ErrorExceptionHandler> _logger;

    public ErrorExceptionHandler(IExceptionToResponseMapper mapper, ILogger<ErrorExceptionHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        var errorResponse = _mapper.Map(exception);
        httpContext.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;
        
        if (response is null)
        {
            return false;
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
