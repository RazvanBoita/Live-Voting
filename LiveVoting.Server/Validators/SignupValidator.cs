using FluentValidation;
using LiveVoting.Shared.Models;

namespace LiveVoting.Server.Validators;

public class SignupValidator : AbstractValidator<SignupModel>
{
    public SignupValidator()
    {
        RuleFor(sm => sm.Email).EmailAddress();
        RuleFor(sm => sm.Cnp).Matches("\\b[1-9][0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12][0-9]|3[01])(?:0[1-9]|[1-3][0-9]|4[0-6]|51|52)[0-9]{4}\\b").WithMessage("CNP has wrong format");
    }
}