namespace LiveVoting.Shared.Dtos;

public class RoundCandidateDto : CandidateDto
{
    public int Votes { get; set; }
    public decimal Percentage { get; set; }
}