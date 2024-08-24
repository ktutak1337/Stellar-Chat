using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StellarChat.Shared.Infrastructure.API.Auth.Jwt;

namespace StellarChat.Shared.Infrastructure.API.Auth;

public static class Extensions
{
    private const string SectionName = "jwt"; 

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<JwtOptions>(section);
        var options = section.BindOptions<JwtOptions>();
          
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = options.ISSUER,
                ValidAudience = options.ISSUER,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(options.SECRET_KEY)), 
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

}
