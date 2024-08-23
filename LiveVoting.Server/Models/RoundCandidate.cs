using Azure.Core.Pipeline;

namespace LiveVoting.Server.Models;


//TODO clasa care defineste relatia de many to many intre candidat si ElectionRound
//TODO Un election round cu numarul 1(turul 1), poate avea mai multi candidati(vreo 15 max parca)
//TODO Un election round cu numarul 2(turul 2), poate avea 2 candidati
public class RoundCandidate
{
    public int RoundCandidateId { get; set; }
    public int CandidateId { get; set; }
    public int ElectionRoundId { get; set; }
    public int Votes { get; set; }
    public decimal Percentage { get; set; }
    public Candidate Candidate { get; set; }
    public ElectionRound ElectionRound { get; set; }
}