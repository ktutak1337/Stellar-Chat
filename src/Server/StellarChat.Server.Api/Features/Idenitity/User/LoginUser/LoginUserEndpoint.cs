namespace StellarChat.Server.Api.Features.Idenitity.User.LoginUser;

internal sealed class LoginUserEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var userManagement = endpoints.MapGroup("/user").WithTags("User Management");

        userManagement.MapPost("/login", [AllowAnonymous] async ([FromBody] LoginUserRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<LoginUser>();

            var response = await mediator.Send(command);

            if (!response.Success)
                return Results.BadRequest(response);

            return Results.Ok(response);
        })
         .Produces(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest) 
         .WithOpenApi(operation => new(operation)
         {
             Summary = "User login."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
