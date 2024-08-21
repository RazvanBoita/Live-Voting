using LiveVoting.Server.Models;

namespace LiveVoting.Server.Repositories.User;

public interface ILoggedUserRepository
{
    public LoggedUser? GetLoggedUser(Guid guid);
    public bool AddUser(LoggedUser loggedUser);
    public bool RemoveUser(LoggedUser loggedUser);
    public bool ExistsUser(LoggedUser loggedUser);
}