namespace StellarChat.Server.Api.Features.Idenitity.User.LoginUser;

internal class LoginUserHandler : ICommandHandler<LoginUser, LoginUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public LoginUserHandler(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async ValueTask<LoginUserResponse> Handle(LoginUser command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user != null)
        {
            if (await _userManager.IsLockedOutAsync(user))
                return new LoginUserResponse(false, "Your account is locked out. Please try again later.", null);

            if (await _userManager.CheckPasswordAsync(user, command.Password))
            {
                await _userManager.ResetAccessFailedCountAsync(user);

                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault();

                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _jwtOptions.SECRET_KEY;

                if (string.IsNullOrEmpty(secretKey))
                    return new LoginUserResponse(false, "Internal server error", null);

                var key = Convert.FromBase64String(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                        new Claim(ClaimTypes.Role, userRole ?? string.Empty),
                        new Claim("scope", "api1")
                    ]),

                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _jwtOptions.ISSUER,
                    Audience = _jwtOptions.ISSUER,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new LoginUserResponse(true, string.Empty, tokenString);
            }
            else
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                    return new LoginUserResponse(false, "Your account is locked out. Please try again later.", null);

                return new LoginUserResponse(false, "Invalid login attempt.", null);
            }
        }

        return new LoginUserResponse(false, "Unauthorized", null);
    }


}
