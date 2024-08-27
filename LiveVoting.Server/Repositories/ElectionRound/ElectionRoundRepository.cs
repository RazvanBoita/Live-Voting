using LiveVoting.Server.Data;
using LiveVoting.Server.Models;

namespace LiveVoting.Server.Repositories.ElectionRound;

public class ElectionRoundRepository : IElectionRoundRepository
{
    private readonly VotingDbContext _dbContext;

    public ElectionRoundRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Models.ElectionRound? GetElectionRoundByElectionIdAndTour(int electionId, int tour)
    {
        return _dbContext.ElectionRounds.FirstOrDefault(er => er.ElectionId == electionId && er.Number == tour);
    }

    public ICollection<RoundCandidate> GetRoundCandidates(int electionRoundId)
    {
        return _dbContext.RoundCandidates.Where(c => c.ElectionRoundId == electionRoundId).ToList();
    }
}