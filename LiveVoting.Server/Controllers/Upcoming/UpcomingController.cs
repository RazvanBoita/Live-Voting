using LiveVoting.Server.Services.UpcomingCandidate;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Upcoming;

[ApiController]
[Route("api/alegeri-2024")]
public class UpcomingController : ControllerBase
{
    private readonly IUpcomingCandidateService _upcomingCandidateService;

    public UpcomingController(IUpcomingCandidateService upcomingCandidateService)
    {
        _upcomingCandidateService = upcomingCandidateService;
    }
    
    
    [HttpGet("candidati")]
    public IActionResult GetAllCandidates()
    {
        return Ok(_upcomingCandidateService.GetCandidates());
    }
}