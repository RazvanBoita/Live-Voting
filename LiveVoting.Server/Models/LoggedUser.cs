namespace LiveVoting.Server.Models;

public class LoggedUser
{
    public int Id { get; set; }
    public Guid LoggedUserId { get; set; }
    public User User { get; set; } = null!;
}