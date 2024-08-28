using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.TestProtected;

[ApiController]
[Route("api/protected")]
public class ProtectedEndpointsController : Controller
{
    [Authorize(Policy = "VerifiedUsersOnly")]
    [HttpGet]
    public IActionResult Get()
    {
        var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
        Console.WriteLine("Auth header:" + authorizationHeader);
        return Ok("Email is verified");
    }

    [Authorize(Policy = "AuthenticatedUsers")]
    [HttpGet("salut")]
    public IActionResult GetAgain()
    {
        var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
        Console.WriteLine("Auth header:" + authorizationHeader);
        return Ok("User is authenticated");
    }
}