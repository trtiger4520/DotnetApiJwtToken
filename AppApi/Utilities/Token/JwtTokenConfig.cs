using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace Utilities.Token;

public record JwtTokenConfig(
    SymmetricSecurityKey SecurityKey,
    string Issuer,
    TimeSpan Expires,
    TimeSpan LongExpires
) : IJwtTokenConfig
{
    public static JwtTokenConfig CreateFromOption(IJwtTokenOptions options)
    {
        if (options.SignKey.Length < 16)
        {
            throw new ArgumentOutOfRangeException(
                nameof(options.SignKey),
                "SignKey MUST be larger than 16 characters");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SignKey));
        var issuer = options.Issuer;
        var expires = TimeSpan.FromMinutes(options.ExpiresMinute);
        var longExpires = TimeSpan.FromMinutes(options.LongExpiresMinute);

        return new JwtTokenConfig(securityKey, issuer, expires, longExpires);
    }
}