using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.TestProtected;

[ApiController]
[Route("/protected")]
public class ProtectedEndpointsController : Controller
{
    [Authorize(Policy = "VerifiedUsersOnly")]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Salut e autorizata treaba aici doar pt aia verificati");
    }

    [Authorize(Policy = "AuthenticatedUsers")]
    [HttpGet("salut")]
    public IActionResult GetAgain()
    {
        return Ok("Salut e autorizata treaba aici da nu prea doar pt aia care au cont practic");
    }
}