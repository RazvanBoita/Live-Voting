namespace LiveVoting.Server.Models;

public class UpcomingCandidate
{
    //Candidatii din 2024
    public int UpcomingCandidateId { get; set; }
    public string Name { get; set; }
    public string Party { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
}