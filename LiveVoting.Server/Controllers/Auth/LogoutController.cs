using LiveVoting.Server.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Auth;

[ApiController]
[Route("/api/logout")]
public class LogoutController : ControllerBase
{
    private readonly IUserService _userService;

    public LogoutController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPost("{userId}")]
    public IActionResult Logout(string userId)
    {
        try
        {
            var convertedGuid = new Guid(userId);
            var foundUser = _userService.GetUserByGuid(convertedGuid);
            if (foundUser is null)
            {
                return BadRequest("User not found, cannot logout.");
            }

            if (!_userService.IsUserLoggedIn(foundUser.UserId))
            {
                return BadRequest("User is not logged in, cannot logout.");
            }
            
            _userService.LogoutUser(foundUser.UserId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
        
        
        return Ok("Logout completed!");
    }
}