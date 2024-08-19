namespace LiveVoting.Server.Services.Jwt;

public interface IJwtService
{
    public string GenerateTokenWithEncodedEmail(string email);
    public string GenerateTokenForLogin(Models.User user);
}