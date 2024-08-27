using LiveVoting.Server.Data;

namespace LiveVoting.Server.Repositories.Election;

public class ElectionRepository : IElectionRepository
{
    private readonly VotingDbContext _dbContext;

    public ElectionRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public Models.Election? GetElectionByYear(int year)
    {
        var stringifiedYear = year.ToString();
        return _dbContext.Elections.FirstOrDefault(e => e.Name.Contains(stringifiedYear));
    }

    public Models.Election? GetElectionById(int id)
    {
        return _dbContext.Elections.Find(id);
    }
}