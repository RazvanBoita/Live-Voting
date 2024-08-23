namespace LiveVoting.Server.Models;

public class ElectionRound
{
    public int ElectionRoundId { get; set; }
    public int ElectionId { get; set; } //fk to Election table
    public int Number { get; set; } //1 sau 2, adica turul 1 sau 2
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalVotes { get; set; }
    public Election Election { get; set; }
    public ICollection<RoundCandidate> Candidates { get; set; } = new List<RoundCandidate>();
}