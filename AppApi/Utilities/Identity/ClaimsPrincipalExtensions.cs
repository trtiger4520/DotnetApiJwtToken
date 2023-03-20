using Utilities.Identity;

namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Get user id
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    public static string? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    /// <summary>
    /// Get user id
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    /// <exception cref="UserIdNotFoundException"></exception>
    public static string GetRequiredUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ??
            throw new UserIdNotFoundException();
    }
    /// <summary>
    /// Get user all roles
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindAll(ClaimTypes.Role).Select(claim => claim.Value);
    }
}
