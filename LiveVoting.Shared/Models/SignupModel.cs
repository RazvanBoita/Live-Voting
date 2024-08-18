using System.ComponentModel.DataAnnotations;

namespace LiveVoting.Shared.Models;

public class SignupModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    
    
    [Required(ErrorMessage = "Username is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "CNP is required")]
    [RegularExpression("\\b[1-9][0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12][0-9]|3[01])(?:0[1-9]|[1-3][0-9]|4[0-6]|51|52)[0-9]{4}\\b", ErrorMessage = "Invalid CNP")]
    public string Cnp { get; set; }
}