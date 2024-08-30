using LiveVoting.Server.Hubs;
using LiveVoting.Server.Services.Jwt;
using LiveVoting.Server.Services.UpcomingCandidate;
using LiveVoting.Server.Services.Voting;
using LiveVoting.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LiveVoting.Server.Controllers.Voting;

[ApiController]
[Route("api/alegeri-2024/voteaza")]
[Authorize(Policy = "VerifiedUsersOnly")]
public class VotingController : ControllerBase
{
    private readonly IUpcomingCandidateService _upcomingCandidateService;
    private readonly IJwtService _jwtService;
    private readonly IVotingService _votingService;
    private readonly IHubContext<VotingHub> _hubContext;

    public VotingController(IUpcomingCandidateService upcomingCandidateService, IJwtService jwtService, IVotingService votingService, IHubContext<VotingHub> hubContext)
    {
        _upcomingCandidateService = upcomingCandidateService;
        _jwtService = jwtService;
        _votingService = votingService;
        _hubContext = hubContext;
    }
    
    
    [HttpPost("{imageUrl}")]
    public async Task<IActionResult> AddVote(string imageUrl)
    {
        var result = _jwtService.ExtractIdFromRequestWithToken(Request);
        if (!result.Item1)
        {
            return Unauthorized(result.Item2);
        }
        
        var votedCandidate = _upcomingCandidateService.GetCandidateByImageUrl(imageUrl);
        
        //convert result.Item2 into guid
        var voteResult = _votingService.AddVote(votedCandidate, new Guid(result.Item2));
        if (!voteResult.Item1)
        {
            return BadRequest(voteResult.Item2);
        }
        
        var newVoteCount = _votingService.GetVotesCountForCandidate(votedCandidate);
        await _hubContext.Clients.All.SendAsync("ReceiveVoteUpdate", imageUrl, newVoteCount);
        return Ok(voteResult.Item2);
    }
    
    [HttpDelete("{imageUrl}")]
    public async Task<IActionResult> DeleteVote(string imageUrl)
    {
        var result = _jwtService.ExtractIdFromRequestWithToken(Request);
        if (!result.Item1)
        {
            return Unauthorized(result.Item2);
        }
        
        var votedCandidate = _upcomingCandidateService.GetCandidateByImageUrl(imageUrl);
        
        //convert result.Item2 into guid
        var voteResult = _votingService.RemoveVote(votedCandidate, new Guid(result.Item2));
        if (!voteResult.Item1)
        {
            return BadRequest(voteResult.Item2);
        }
        
        var newVoteCount = _votingService.GetVotesCountForCandidate(votedCandidate);
        await _hubContext.Clients.All.SendAsync("ReceiveVoteUpdate", imageUrl, newVoteCount);
        return Ok(voteResult.Item2);
    }

    [HttpGet]
    public IActionResult GetVotedCandidate()
    {
        //get the id from request
        var result = _jwtService.ExtractIdFromRequestWithToken(Request);
        if (!result.Item1)
        {
            return Unauthorized(result.Item2);
        }
        
        //get the candidate they voted
        var votedCandidate = _votingService.GetVotedCandidateForUserId(new Guid(result.Item2));
        if (votedCandidate is null)
        {
            return BadRequest("Voted candidate not found");
        }

        var newDto = new CandidateDto
        {
            Bio = votedCandidate.Bio,
            ImageUrl = votedCandidate.ImageUrl,
            Name = votedCandidate.Name,
            Party = votedCandidate.Party
        };
        
        return Ok(newDto);
    }
}