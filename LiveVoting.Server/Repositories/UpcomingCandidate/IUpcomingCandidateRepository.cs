namespace LiveVoting.Server.Repositories.UpcomingCandidate;

public interface IUpcomingCandidateRepository
{
    public IEnumerable<Models.UpcomingCandidate> GetAllUpcomingCandidates();
    public Models.UpcomingCandidate? GetUpcomingCandidateById(int id);
    public Models.UpcomingCandidate? GetUpcomingCandidateByName(string name);
    
    //Ciudat, dar ma ajuta la votare ...
    public Models.UpcomingCandidate? GetUpcomingCandidateByImageUrl(string imageUrl);
    
}