namespace StellarChat.Server.Api.Features.Idenitity.User.RegistrationUser;

internal class RegistrationUserHandler : ICommandHandler<RegistrationUser, RegistrationUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager; 
    private readonly ILogger<RegistrationUserHandler> _logger;

    public RegistrationUserHandler(
        UserManager<ApplicationUser> userManager, 
        ILogger<RegistrationUserHandler> logger)
    {
        _userManager = userManager; 
        _logger = logger;
    }

    public async ValueTask<RegistrationUserResponse> Handle(RegistrationUser command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering a user!");

        var user = new ApplicationUser { UserName = command.Username, Email = command.Email, RegistredOn = DateTime.Now };
        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, StellarRoles.Basic);

            return new RegistrationUserResponse(
                Success: true,
                "User registered successfully",
                user.UserName,
                user.Email);
        }
        var errorDescriptions = string.Join(", ", result.Errors.Select(e => e.Description));

        _logger.LogWarning($"User registration failed. {errorDescriptions}");

        return new RegistrationUserResponse(
            Success: false,
            $"User registration failed. {errorDescriptions}",
            null,
            null);
    }


}
