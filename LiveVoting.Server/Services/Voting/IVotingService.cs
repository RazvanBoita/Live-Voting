namespace LiveVoting.Server.Services.Voting;

public interface IVotingService
{
    public (bool, string) AddVote(Models.UpcomingCandidate? upcomingCandidate, Guid voterId);
    public (bool, string) RemoveVote(Models.UpcomingCandidate? upcomingCandidate, Guid voterId);
    public Models.UpcomingCandidate? GetVotedCandidateForUserId(Guid guid);
    public int GetVotesCountForCandidate(Models.UpcomingCandidate? candidate);
}