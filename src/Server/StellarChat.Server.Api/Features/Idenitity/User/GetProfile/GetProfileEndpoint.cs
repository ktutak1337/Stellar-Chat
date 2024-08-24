namespace StellarChat.Server.Api.Features.Idenitity.User.GetProfile;

internal sealed class GetProfileEndpoint : IEndpoint
{
    public void Expose(IEndpointRouteBuilder endpoints)
    {
        var userManagement = endpoints.MapGroup("/user").WithTags("User Management");

        userManagement.MapGet("/profile", [Authorize] async (IMediator mediator) =>
        {  
            var response = await mediator.Send(new GetProfile());

            if (!response.Success)
                return Results.BadRequest(response);

            return Results.Ok(response);
        })
         .Produces(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest) 
         .WithName("GetUserProfile")
         .WithOpenApi(operation => new(operation)
         {
             Summary = "Retrieves the user profile of the logged in user."
         }); 
    }

    public void Register(IServiceCollection services, IConfiguration configuration) { }

    public void Use(IApplicationBuilder app) { }
}
