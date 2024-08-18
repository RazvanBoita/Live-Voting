using LiveVoting.Shared.Models;

namespace LiveVoting.Server.Services.User;

public interface IUserService
{
    public bool UserExists(string emailAddress);
    public bool InsertUser(SignupModel signupModel);
}