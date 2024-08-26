using LiveVoting.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace LiveVoting.Server.Repositories.Candidate;

public class CandidateRepository : ICandidateRepository
{
    
    private readonly VotingDbContext _dbContext;

    public CandidateRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<IEnumerable<Models.Candidate>> GetCandidatesAsync()
    {
        return await _dbContext.Candidates.ToListAsync();
    }

    public Models.Candidate? GetCandidate(int id)
    {
        return _dbContext.Candidates.Find(id);
    }
}