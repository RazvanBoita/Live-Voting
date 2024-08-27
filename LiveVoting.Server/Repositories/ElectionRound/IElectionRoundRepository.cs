namespace LiveVoting.Server.Repositories.ElectionRound;

public interface IElectionRoundRepository
{
    public Models.ElectionRound? GetElectionRoundByElectionIdAndTour(int electionId, int tour);
    public ICollection<Models.RoundCandidate> GetRoundCandidates(int electionRoundId);
}