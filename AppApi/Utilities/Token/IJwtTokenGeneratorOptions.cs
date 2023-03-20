namespace Utilities.Token;

public interface IJwtTokenOptions
{
    string Issuer { get; set; }
    uint ExpiresMinute { get; set; }
    string SignKey { get; set; }
    double LongExpiresMinute { get; set; }
}