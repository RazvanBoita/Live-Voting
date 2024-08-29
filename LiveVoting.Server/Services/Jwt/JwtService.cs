using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LiveVoting.Server.Services.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private const string Issuer = "LiveVoting.Server";
    private const string Audience = "LiveVoting.Server";
    
    public string GenerateTokenWithEncodedEmail(string email)
    {
        var jwtSecret = _configuration["JwtSecret"] ?? throw new ApplicationException("Missing JWT Secret");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: new[] {new Claim("email", email)},
            expires: DateTime.Now.AddHours(6),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateTokenForLogin(Models.User user)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("IsEmailConfirmed", user.ConfirmedEmail.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecret"] ?? throw new ApplicationException("Missing JWT Secret")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(48),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (bool, string) ExtractEmailFromRequestWithToken(HttpRequest request)
    {
        var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
        {
            return (false, "Token is missing or invalid.");
        }
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (email is null)
        {
            return (false, "Email cant be retrieved from token.");
        }
        
        return (true, email);
    }

    public (bool, string) ExtractIdFromRequestWithToken(HttpRequest request)
    {
        var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
        {
            return (false, "Token is missing or invalid.");
        }
        
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var guid = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        
        if (guid is null)
        {
            return (false, "User id cant be retrieved from token.");
        }
        
        return (true, guid);
    }
}