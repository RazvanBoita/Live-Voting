namespace LiveVoting.Server.Models;

public class Election
{
    public int ElectionId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool HasFinished { get; set; }
}