namespace LiveVoting.Server.Repositories.User;

public interface IUserRepository
{
    public Models.User? GetUserById(Guid userId);
    public Models.User? GetUserByEmail(string emailAddress);
    public bool UserExistsById(Guid userId);
    public bool UserExistsByEmail(string emailAddress);
    public bool InsertUser(Models.User user);
    public bool DeleteUser(Models.User user);
}