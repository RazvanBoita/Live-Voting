using LiveVoting.Server.Services.Jwt;
using LiveVoting.Server.Services.User;
using LiveVoting.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Auth;

[ApiController]
[Route("/api/login")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public LoginController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }
    
    
    [HttpPost]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var foundUser = _userService.GetUserByEmail(loginModel.Email);
        
        if (foundUser is null)
        {
            return BadRequest("Login failed. Check your credentials and try again(email).");
        }

        if (_userService.IsUserLoggedIn(foundUser.UserId))
        {
            return BadRequest("Login failed. User already logged in current session.");
        }
        
        if (!_userService.VerifyCredentials(loginModel))
        {
            return BadRequest("Login failed. Check your credentials and try again.");
        }
        
        
        var generatedJwt = _jwtService.GenerateTokenForLogin(foundUser);
        _userService.LoginUser(foundUser.UserId);
        
        return Ok(generatedJwt);
    }
    
}