namespace Utilities.Token;

public class JwtTokenOptions : IJwtTokenOptions
{
    public string Issuer { get; set; } = null!;
    public uint ExpiresMinute { get; set; } = 10;
    public string SignKey { get; set; } = null!;
    public double LongExpiresMinute { get; set; } = 30;
}
