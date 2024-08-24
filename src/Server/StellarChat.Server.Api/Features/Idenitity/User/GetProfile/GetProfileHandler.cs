namespace StellarChat.Server.Api.Features.Idenitity.User.GetProfile;

internal class GetProfileHandler : ICommandHandler<GetProfile, GetProfileResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public GetProfileHandler(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async ValueTask<GetProfileResponse> Handle(GetProfile command, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) 
            return new GetProfileResponse { Success = false, Message = "User not found" }; 
        
        var userRoles = await _userManager.GetRolesAsync(user);

        return new GetProfileResponse
        { 
            Success = true,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            IsPhoneNumberConfirmed = user.PhoneNumberConfirmed, 
            Role = userRoles.FirstOrDefault()
        }; 
    }


}
