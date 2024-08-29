using LiveVoting.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace LiveVoting.Server.Repositories.UpcomingVote;

public class UpcomingVoteRepository : IUpcomingVoteRepository
{
    private readonly VotingDbContext _dbContext;
    private static readonly object _lockObject = new object();

    public UpcomingVoteRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Models.UpcomingVote? GetVoteByGuid(Guid guid)
    {
        return _dbContext.UpcomingVotes.FirstOrDefault(uv => uv.UserId == guid);
    }
    

    public bool AddVote(Models.UpcomingVote upcomingVote)
    {
        lock (_lockObject)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var existingVote = _dbContext.UpcomingVotes.FirstOrDefault(uv => uv.UserId == upcomingVote.UserId);
                    if (existingVote != null)
                    {
                        return false;
                    }
                
                    _dbContext.UpcomingVotes.Add(upcomingVote);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    Console.WriteLine("Concurrency error occurred when adding upcoming vote: " + e.Message);
                    return false;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error occurred when adding upcoming vote: " + e.Message);
                    return false;
                }
            }
        }
    }


    public bool RemoveVote(Models.UpcomingVote upcomingVote)
    {
        try
        {
            _dbContext.UpcomingVotes.Remove(upcomingVote);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured when removing upcoming vote: " + e.Message);
            return false;
        }
    }

    public bool UserVotedAlready(Guid guid)
    {
        var foundVote = _dbContext.UpcomingVotes.FirstOrDefault(uv => uv.UserId == guid);
        return foundVote is not null;
    }

    public bool ExistsPairOfUserIdAndUpcomingCandidate(Guid userId, Models.UpcomingCandidate candidate)
    {
        return _dbContext.UpcomingVotes.Any(uv =>
            uv.UserId == userId && uv.UpcomingCandidateId == candidate.UpcomingCandidateId);
    }

    public int GetVotesCountForUpcomingCandidateId(int candidateId)
    {
        return _dbContext.UpcomingVotes.Count(upc => upc.UpcomingCandidateId == candidateId);
    }
}