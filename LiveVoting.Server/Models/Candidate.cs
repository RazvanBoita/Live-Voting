namespace LiveVoting.Server.Models;

public class Candidate
{
    public int CandidateId { get; set; }
    public string Name { get; set; }
    public string Party { get; set; }
    public string Bio { get; set; }
}