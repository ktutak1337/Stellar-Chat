namespace StellarChat.Server.Api.Features.Idenitity.User.RegistrationUser;

internal sealed class RegistrationUserEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var userManagement = endpoints.MapGroup("/user").WithTags("User Management");

        userManagement.MapPost("/register", [AllowAnonymous] async ([FromBody] RegistrationUserRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<RegistrationUser>();

            var response = await mediator.Send(command);

            if (!response.Success)
                return Results.BadRequest(response);

            return Results.Ok(response);
        })
         .Produces(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest) 
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Registration new anonimus user."
         });
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
