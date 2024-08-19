using LiveVoting.Shared.Models;

namespace LiveVoting.Server.Services.User;

public interface IUserService
{
    public Models.User? GetUserByEmail(string email);
    public bool UserExists(string emailAddress);
    public Task InsertUser(SignupModel signupModel);
    public bool ConfirmUserEmail(Models.User user);
    public bool CnpExists(string cnp);
    public bool VerifyCredentials(LoginModel loginModel);
}