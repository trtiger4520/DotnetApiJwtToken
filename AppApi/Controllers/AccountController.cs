using System.Security.Claims;

using AppApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Utilities.Token;

namespace AppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AccountController(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var data = GenerateToken(request.UserName, request.RememberMe);
        return Ok(data);
    }

    private LoginResponse GenerateToken(string userName, bool rememberMe)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "RoleA"),
            new Claim(ClaimTypes.Role, "RoleB"),
        };

        var token = _tokenGenerator.GenerateToken(new ClaimsIdentity(claims), rememberMe);
        return new LoginResponse()
        {
            Token = token
        };
    }

    [Authorize]
    [HttpGet("whoami")]
    public IActionResult WhoAmI()
    {
        var data = new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated,
            Id = User.GetRequiredUserId(),
            Name = User.Identity?.Name,
            Roles = User.GetRoles(),
        };

        return Ok(data);
    }
}