namespace LiveVoting.Server.Models;

public class ElectionRound
{
    public int ElectionRoundId { get; set; }
    public int ElectionId { get; set; } //fk to Election table
    public int Number { get; set; } //1 sau 2, adica turul 1 sau 2
    
}