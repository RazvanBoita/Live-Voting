using FluentValidation;
using LiveVoting.Server.Services.Email;
using LiveVoting.Server.Services.User;
using LiveVoting.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiveVoting.Server.Controllers.Auth;

[ApiController]
[Route("api/signup")]
public class SignupController : ControllerBase
{
    private readonly IValidator<SignupModel> _signupModelValidator;
    private readonly IUserService _userService;
    private readonly IConfirmationEmailSender _emailSender;
    
    public SignupController(IValidator<SignupModel> signupModelValidator, IUserService userService, IConfirmationEmailSender emailSender)
    {
        _userService = userService;
        _signupModelValidator = signupModelValidator;
        _emailSender = emailSender;
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
    {
        var responseStatus = _signupModelValidator.Validate(signupModel);
        if (!responseStatus.IsValid)
        {
            return BadRequest(responseStatus.Errors);
        }

        if (_userService.UserExists(signupModel.Email))
        {
            return BadRequest("Email already exists"); 
        }

        if (_userService.CnpExists(signupModel.Cnp))
        {
            return BadRequest("Cnp already assigned to another account");
        }
        
        await _userService.InsertUser(signupModel);
        
        await _emailSender.SendAccountConfirmationEmail(signupModel.Email);
        
        return Ok("Email sent to: " + signupModel.Email);
    }
}