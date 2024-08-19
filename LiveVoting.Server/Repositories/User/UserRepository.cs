using LiveVoting.Server.Data;

namespace LiveVoting.Server.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly VotingDbContext _dbContext;

    public UserRepository(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Models.User? GetUserById(Guid userId)
    {
        return _dbContext.Users.Find(userId);
    }

    public Models.User? GetUserByEmail(string emailAddress)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == emailAddress);
    }

    public bool UserExistsById(Guid userId)
    {
        return _dbContext.Users.Any(u => u.UserId == userId);
    }

    public bool UserExistsByEmail(string emailAddress)
    {
        return _dbContext.Users.Any(u => u.Email == emailAddress);
    }

    public bool InsertUser(Models.User user)
    {
        try
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public bool DeleteUser(Models.User user)
    {
        try
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public bool ConfirmEmailForUser(Models.User user)
    {
        try
        {
            user.ConfirmedEmail = true;
            // _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            user.ConfirmedEmail = false;
            _dbContext.SaveChanges();
            return false;
        }

        return true;
    }

    public Models.User? GetUserByCnp(string cnp){
        return _dbContext.Users.FirstOrDefault(u => u.CNP == cnp);
    }

}