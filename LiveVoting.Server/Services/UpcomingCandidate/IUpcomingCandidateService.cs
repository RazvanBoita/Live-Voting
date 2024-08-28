using LiveVoting.Shared.Dtos;

namespace LiveVoting.Server.Services.UpcomingCandidate;

public interface IUpcomingCandidateService
{
    public ICollection<CandidateDto> GetCandidates();
    public CandidateDto? GetCandidate(string name);
    public CandidateDto? GetCandidate(int candidateId);
}