using LiveVoting.Shared.Dtos;

namespace LiveVoting.Server.Services.Candidate;

public interface ICandidateService
{
    public Task<ICollection<CandidateDto>> GetCandidatesAsync();
    public CandidateDto? GetCandidate(int id);
}