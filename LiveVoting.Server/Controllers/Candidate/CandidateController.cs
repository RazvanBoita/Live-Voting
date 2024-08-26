using LiveVoting.Server.Services.Candidate;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Candidate;


[ApiController]
[Route("api/candidates")]
public class CandidateController : ControllerBase
{
    
    private readonly ICandidateService _candidateService;

    public CandidateController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCandidatesAsync()
    {
        var candidates = await _candidateService.GetCandidatesAsync();
        return Ok(candidates);
    }

    [HttpGet("{id}")]
    public IActionResult GetCandidateById(int id)
    {
        var foundCandidate = _candidateService.GetCandidate(id);
        if (foundCandidate is null)
        {
            return NotFound($"Candidate with id {id} not found");
        }

        return Ok(foundCandidate);
    }
}