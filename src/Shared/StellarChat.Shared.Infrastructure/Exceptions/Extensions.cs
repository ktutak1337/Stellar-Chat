using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StellarChat.Shared.Abstractions.Exceptions;

namespace StellarChat.Shared.Infrastructure.Exceptions;
public static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
            .AddExceptionHandler<ErrorExceptionHandler>()
            .AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseExceptionHandler(_ => { });
}
