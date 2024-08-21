using LiveVoting.Server.Data;
using LiveVoting.Server.Models;

namespace LiveVoting.Server.Repositories.User;

public class LoggedUserRepository : ILoggedUserRepository
{
    private readonly VotingDbContext _dbContext;

    public LoggedUserRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public LoggedUser? GetLoggedUser(Guid guid)
    {
        return _dbContext.LoggedUsers.FirstOrDefault(lu => lu.LoggedUserId == guid);
    }

    public bool AddUser(LoggedUser loggedUser)
    {
        try
        {
            _dbContext.LoggedUsers.Add(loggedUser);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public bool RemoveUser(LoggedUser loggedUser)
    {
        try
        {
            _dbContext.LoggedUsers.Remove(loggedUser);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    public bool ExistsUser(LoggedUser loggedUser)
    {
        return _dbContext.LoggedUsers.Any(u => u.LoggedUserId == loggedUser.LoggedUserId);
    }
}