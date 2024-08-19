using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LiveVoting.Server.Services.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LiveVoting.Server.Controllers.Email;

[ApiController]
[Route("/api/verify")]
public class VerifyEmailController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public VerifyEmailController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult Verify(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecret = _configuration["JwtSecret"] ?? throw new Exception("Jwt secret not found");
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        try
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var email = jwtToken.Claims.First(x => x.Type == "email").Value;
            
            var foundUser = _userService.GetUserByEmail(email);
            if (foundUser is null)
            {
                return Unauthorized("Email could not be verified and extracted from token.");
            }

            if (foundUser.ConfirmedEmail)
            {
                return BadRequest("User already verified their email.");
            }
            
            var resultToConfirmation = _userService.ConfirmUserEmail(foundUser);
            if (resultToConfirmation is false)
            {
                return Unauthorized("Something went wrong when confirming email address");
            }
            
            
            return Ok("Email confirmed for user " + email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}