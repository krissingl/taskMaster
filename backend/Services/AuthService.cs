//

public class AuthService
{
    private readonly IConfiguration _config;
    private static List<User> _users = new();

    public AuthService(IConfiguration config)
    {
        _config = config;
    }
}