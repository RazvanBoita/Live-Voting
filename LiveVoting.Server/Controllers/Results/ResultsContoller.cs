using LiveVoting.Server.Repositories.Election;
using LiveVoting.Server.Repositories.ElectionRound;
using LiveVoting.Server.Services.Election;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Results;


[ApiController]
[Route("api/rezultate")]
public class ResultsContoller : ControllerBase
{
    
    private readonly IElectionService _electionService;

    public ResultsContoller(IElectionService electionService)
    {
        _electionService = electionService;
    }

    [HttpGet("{year}/{tour}")]
    public IActionResult GetResultsForYearAndTour(int year, int tour)
    {
        var result = _electionService.GetCandidatesForYearAndTour(year, tour);
        if (result is null)
        {
            return BadRequest("Something went wrong");
        }
        return Ok(result);
    }
    
    
    [HttpGet("contor/{year}/{tour}")]
    public IActionResult GetResultsCountForYearAndTour(int year, int tour)
    {
        var result = _electionService.GetTotalVotesForYearAndTour(year, tour);
        if (result == -1)
        {
            return BadRequest("No election found");
        }

        if (result == -2)
        {
            return BadRequest("Invalid tour");
        }
        return Ok(result);
    }
    
}