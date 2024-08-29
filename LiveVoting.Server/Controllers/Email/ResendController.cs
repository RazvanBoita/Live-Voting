using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LiveVoting.Server.Services.Email;
using LiveVoting.Server.Services.Jwt;
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
    private readonly IJwtService _jwtService;

    public ResendController(IConfirmationEmailSender emailSender, IJwtService jwtService)
    {
        _emailSender = emailSender;
        _jwtService = jwtService;
    }
    
    [HttpGet]
    public async Task<IActionResult> ResendEmail()
    {
        var result = _jwtService.ExtractEmailFromRequestWithToken(Request);
        if (!result.Item1)
        {
            return Unauthorized(result.Item2);
        }
        await _emailSender.SendAccountConfirmationEmail(result.Item2);
        
        return Ok($"Verification email sent to {result.Item2}.");
    }
}