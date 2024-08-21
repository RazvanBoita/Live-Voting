using LiveVoting.Server.Models;
using LiveVoting.Server.Repositories.User;
using LiveVoting.Server.Services.Hashing;
using LiveVoting.Shared.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace LiveVoting.Server.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly ILoggedUserRepository _loggedUserRepository;
    public UserService(IUserRepository userRepository, IHashingService hashingService, ILoggedUserRepository loggedUserRepository)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _loggedUserRepository = loggedUserRepository;
    }
    
    public bool UserExists(string emailAddress)
    {
        return _userRepository.UserExistsByEmail(emailAddress);
    }

    public async Task InsertUser(SignupModel signupModel)
    {
        var newUser = new Models.User()
        {
            Email = signupModel.Email,
            CNP = signupModel.Cnp,
            ConfirmedEmail = false,
            FirstName = signupModel.FirstName,
            Surname = signupModel.Surname,
            UserId = Guid.NewGuid(),
            PasswordHash = _hashingService.Hash(signupModel.Password)
        };
        _userRepository.InsertUser(newUser);
    }

    public Models.User? GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public Models.User? GetUserByGuid(Guid guid)
    {
        return _userRepository.GetUserById(guid);
    }

    public bool ConfirmUserEmail(Models.User user)
    {
        return _userRepository.ConfirmEmailForUser(user);
    }

    public bool CnpExists(string cnp)
    {
        return _userRepository.GetUserByCnp(cnp) != null;
    }

    public bool VerifyCredentials(LoginModel loginModel)
    {
        var foundUser = _userRepository.GetUserByEmail(loginModel.Email);
        return _hashingService.Verify(loginModel.Password, foundUser!.PasswordHash);
    }

    public bool LoginUser(Guid userId)
    {
        return _loggedUserRepository.AddUser(new LoggedUser{LoggedUserId = userId});
    }

    public bool LogoutUser(Guid guid)
    {
        var foundUser = _loggedUserRepository.GetLoggedUser(guid);
        return _loggedUserRepository.RemoveUser(foundUser!);
    }

    public bool IsUserLoggedIn(Guid userId)
    {
        return _loggedUserRepository.ExistsUser(new LoggedUser{LoggedUserId = userId});
    }
}