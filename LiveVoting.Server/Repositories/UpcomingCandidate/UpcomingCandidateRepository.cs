using LiveVoting.Server.Data;

namespace LiveVoting.Server.Repositories.UpcomingCandidate;

public class UpcomingCandidateRepository : IUpcomingCandidateRepository
{
    private readonly VotingDbContext _dbContext;

    public UpcomingCandidateRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public IEnumerable<Models.UpcomingCandidate> GetAllUpcomingCandidates()
    {
        return _dbContext.UpcomingCandidates.ToList();
    }

    public Models.UpcomingCandidate? GetUpcomingCandidateById(int id)
    {
        return _dbContext.UpcomingCandidates.Find(id);
    }

    public Models.UpcomingCandidate? GetUpcomingCandidateByName(string name)
    {
        return _dbContext.UpcomingCandidates.FirstOrDefault(uc => uc.Name.ToLower().Contains(name.ToLower()));
    }

    public Models.UpcomingCandidate? GetUpcomingCandidateByImageUrl(string imageUrl)
    {
        return _dbContext.UpcomingCandidates.FirstOrDefault(uc => String.Equals(uc.ImageUrl.ToLower(), imageUrl.ToLower()));
    }
}