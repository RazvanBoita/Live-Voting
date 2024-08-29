using LiveVoting.Server.Repositories.UpcomingCandidate;
using LiveVoting.Server.Services.Voting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Results;


[ApiController]
[Route("api/provizorii")]
[Authorize(Policy = "AuthenticatedUsers")]
public class UpcomingResultsController : ControllerBase
{
    private readonly IVotingService _votingService;
    private readonly IUpcomingCandidateRepository _upcomingCandidateRepository;
    public UpcomingResultsController(IVotingService votingService, IUpcomingCandidateRepository upcomingCandidateRepository)
    {
        _votingService = votingService;
        _upcomingCandidateRepository = upcomingCandidateRepository;
    }
    
    
    [HttpGet("{imageUrl}")]
    public IActionResult GetVotesCount(string imageUrl)
    {
        var foundCandidate = _upcomingCandidateRepository.GetUpcomingCandidateByImageUrl(imageUrl);
        if (foundCandidate is null)
        {
            return NotFound("Candidate not found");
        }
        
        var result = _votingService.GetVotesCountForCandidate(foundCandidate);
        return Ok(result);
    }
    
}