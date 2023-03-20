using Microsoft.IdentityModel.Tokens;

namespace Utilities.Token;

public interface IJwtTokenConfig
{
    SymmetricSecurityKey SecurityKey { get; init; }
    string Issuer { get; init; }
    TimeSpan Expires { get; init; }
    TimeSpan LongExpires { get; init; }
}
