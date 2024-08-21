using LiveVoting.Shared.Models;

namespace LiveVoting.Server.Services.User;

public interface IUserService
{
    public Models.User? GetUserByEmail(string email);
    public Models.User? GetUserByGuid(Guid guid);
    public bool UserExists(string emailAddress);
    public Task InsertUser(SignupModel signupModel);
    public bool ConfirmUserEmail(Models.User user);
    public bool CnpExists(string cnp);
    public bool VerifyCredentials(LoginModel loginModel);
    public bool LoginUser(Guid userId);
    public bool LogoutUser(Guid userId);
    public bool IsUserLoggedIn(Guid userId);
    
}