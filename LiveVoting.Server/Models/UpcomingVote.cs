namespace LiveVoting.Server.Models;

public class UpcomingVote
{
    public int UpcomingVoteId { get; set; }
    
    public Guid UserId { get; set; } //fk to the user table. adica asta e cel care a votat
    public User User { get; set; }
    
    public int UpcomingCandidateId { get; set; }
    public UpcomingCandidate UpcomingCandidate { get; set; }
}