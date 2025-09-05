using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly IConfiguration _config;
    private static List<User> _users = new();

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public bool UserExists(string username)
    {
        return _users.Any(u => u.Username == username);
    }

    public User Register(string username, string password)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User { Id = _users.Count + 1, Username = username, PasswordHash = hashed };
        _users.Add(user);
        return user;
    }

    public string? Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }
        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}