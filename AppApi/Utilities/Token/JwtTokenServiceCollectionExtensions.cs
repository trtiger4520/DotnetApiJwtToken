using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Utilities.Token;

namespace Microsoft.Extensions.DependencyInjection;

public static class JwtTokenServiceCollectionExtensions
{
    public static AuthenticationBuilder AddJwtAuthentication(
        this IServiceCollection services,
        Action<IJwtTokenOptions> setupOptions)
    {
        var options = new JwtTokenOptions();

        setupOptions?.Invoke(options);

        var config = JwtTokenConfig.CreateFromOption(options);

        services.AddSingleton<IJwtTokenConfig>(config);
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,

                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = config.Issuer,
                    IssuerSigningKey = config.SecurityKey,
                };
            });
    }
}