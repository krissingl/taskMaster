using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] LoginRequest req)
    {
        if (_authService.UserExists(req.Username))
        {
            return BadRequest("Username already exists.");
        }

        _authService.Register(req.Username, req.Password);
        return Ok("User registered.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var token = _authService.Login(req.Username, req.Password);
        if (token == null)
        {
            return Unauthorized("Invalid username or password");
        }
        return Ok(new { token });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}