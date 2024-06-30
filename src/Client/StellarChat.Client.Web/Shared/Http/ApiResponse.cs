namespace StellarChat.Client.Web.Shared.Http;

public record ApiResponse(HttpResponseMessage HttpResponse, bool Succeeded, ErrorsResponse? Errors = null);

public record ApiResponse<T>(T Value, HttpResponseMessage HttpResponse, bool Succeeded, ErrorsResponse? Errors = null) : ApiResponse(HttpResponse, Succeeded, Errors);

public record ApiError(string Code, string Message, string? UserMessage);

public record ErrorsResponse(ApiError[] Errors);
