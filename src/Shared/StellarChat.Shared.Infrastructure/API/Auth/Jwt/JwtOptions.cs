namespace StellarChat.Shared.Infrastructure.API.Auth.Jwt;

public class JwtOptions
{  
    public string SECRET_KEY { get; set; } = string.Empty;
    public string ISSUER { get; set; } = string.Empty;
    public int TOKEN_EXPIRATION_MINUTES { get; set; } 
    public int REFRESH_TOKEN_EXPIRATION_DAYS { get; set; } 
}
