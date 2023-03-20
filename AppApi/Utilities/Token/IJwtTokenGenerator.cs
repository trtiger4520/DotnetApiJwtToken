using System.Security.Claims;

namespace Utilities.Token;

public interface IJwtTokenGenerator
{
    string GenerateToken(ClaimsIdentity claimsIdentity, bool useLongExpires = false);
}
