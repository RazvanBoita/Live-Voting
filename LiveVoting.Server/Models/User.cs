namespace LiveVoting.Server.Models;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string CNP { get; set; }
    public bool ConfirmedEmail { get; set; }
    public LoggedUser? LoggedUser { get; set; }
    public UpcomingVote UpcomingVote { get; set; }
}