using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LiveVoting.Server.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Email;


[ApiController]
[Route("api/resend")]
[Authorize(Policy = "AuthenticatedUsers")]
public class ResendController : ControllerBase
{
    private readonly IConfirmationEmailSender _emailSender;

    public ResendController(IConfirmationEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    
    [HttpGet]
    public async Task<IActionResult> ResendEmail()
    {
        //extrage tokenu
        var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized("Token is missing or invalid.");
        }
        
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        // Decode jwt
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (email is null)
        {
            return Unauthorized("Email cant be retrieved from token.");
        }
        
        await _emailSender.SendAccountConfirmationEmail(email);
        
        return Ok($"Verification email sent to {email}.");
    }
}