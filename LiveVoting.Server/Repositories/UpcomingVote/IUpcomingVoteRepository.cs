namespace LiveVoting.Server.Repositories.UpcomingVote;

public interface IUpcomingVoteRepository
{
    public Models.UpcomingVote? GetVoteByGuid(Guid guid);
    public bool AddVote(Models.UpcomingVote upcomingVote);
    public bool RemoveVote(Models.UpcomingVote upcomingVote);
    public bool UserVotedAlready(Guid guid);
    public bool ExistsPairOfUserIdAndUpcomingCandidate(Guid userId, Models.UpcomingCandidate candidate);
    public int GetVotesCountForUpcomingCandidateId(int candidateId);
}