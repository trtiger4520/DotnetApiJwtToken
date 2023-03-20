using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AppApi.Utilities.Date;

namespace Utilities.Token;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IJwtTokenConfig _config;
    private readonly IDateTimeProvider? _dateTimeProvider;

    public JwtTokenGenerator(IJwtTokenConfig config, IDateTimeProvider? dateTimeProvider = null)
    {
        _config = config;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(ClaimsIdentity claimsIdentity, bool useLongExpires = false)
    {
        claimsIdentity.AddClaim(
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var signingCredentials = new SigningCredentials(
            _config.SecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var now = _dateTimeProvider?.Now() ?? DateTime.Now;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _config.Issuer,
            Subject = claimsIdentity,
            Expires = useLongExpires
                ? now.Add(_config.LongExpires)
                : now.Add(_config.Expires),
            SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
