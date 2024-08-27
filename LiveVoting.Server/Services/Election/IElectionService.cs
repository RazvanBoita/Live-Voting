using LiveVoting.Shared.Dtos;

namespace LiveVoting.Server.Services.Election;

public interface IElectionService
{
    //rezultate tur 1 (lista de candidati)
    public IEnumerable<RoundCandidateDto> GetCandidatesForYearAndTour(int year, int tour);
    
    //rezultate tur 2 (lista de candidati)
    
    //total votes no
    public int GetTotalVotesForYearAndTour(int year, int tour);
}