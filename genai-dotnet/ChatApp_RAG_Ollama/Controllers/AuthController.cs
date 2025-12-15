using Microsoft.AspNetCore.Mvc;
using ChatApp_RAG_Ollama.Models;
using ChatApp_RAG_Ollama.Services;

namespace ChatApp_RAG_Ollama.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;

    public AuthController(AuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password are required");
        }

        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var token = _jwtService.GenerateToken(user);
        var response = new LoginResponse
        {
            Token = token,
            Expires = DateTime.UtcNow.AddHours(24),
            Username = user.Username
        };

        return Ok(response);
    }

    [HttpPost("validate")]
    public IActionResult ValidateToken([FromBody] string token)
    {
        var principal = _jwtService.ValidateToken(token);
        if (principal == null)
        {
            return Unauthorized("Invalid token");
        }

        return Ok(new { Valid = true, Username = principal.Identity?.Name });
    }
}






