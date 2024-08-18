using LiveVoting.Server.Repositories.User;
using LiveVoting.Server.Services.Hashing;
using LiveVoting.Shared.Models;

namespace LiveVoting.Server.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    public UserService(IUserRepository userRepository, IHashingService hashingService)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
    }
    
    public bool UserExists(string emailAddress)
    {
        return _userRepository.UserExistsByEmail(emailAddress);
    }

    public bool InsertUser(SignupModel signupModel)
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
        return _userRepository.InsertUser(newUser);
    }
}