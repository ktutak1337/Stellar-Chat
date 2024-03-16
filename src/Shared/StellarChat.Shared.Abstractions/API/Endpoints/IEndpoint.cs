using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StellarChat.Shared.Abstractions.API.Endpoints;

public interface IEndpoint
{
    void Register(IServiceCollection services, IConfiguration configuration);
    void Use(IApplicationBuilder app);
    void Expose(IEndpointRouteBuilder endpoints);

    protected static IResult Select<TData>(TData data)
        where TData : class
            => data is null
                ? Results.NotFound()
                : Results.Ok(data);
}