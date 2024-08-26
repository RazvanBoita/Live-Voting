namespace LiveVoting.Server.Repositories.Candidate;

public interface ICandidateRepository
{
    public Task<IEnumerable<Models.Candidate>> GetCandidatesAsync();
    public Models.Candidate? GetCandidate(int id);
}