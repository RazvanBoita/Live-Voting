using LiveVoting.Server.Models;
using LiveVoting.Server.Repositories.Candidate;
using LiveVoting.Server.Repositories.Election;
using LiveVoting.Server.Repositories.ElectionRound;
using LiveVoting.Shared.Dtos;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp;

namespace LiveVoting.Server.Services.Election;

public class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;
    private readonly IElectionRoundRepository _electionRoundRepository;
    private readonly ICandidateRepository _candidateRepository;

    public ElectionService(IElectionRepository electionRepository, IElectionRoundRepository electionRoundRepository, ICandidateRepository candidateRepository)
    {
        _electionRepository = electionRepository;
        _electionRoundRepository = electionRoundRepository;
        _candidateRepository = candidateRepository;
    }

    public int GetTotalVotesForYearAndTour(int year, int tour)
    {
        if (tour != 1 && tour != 2)
        {
            return -2;
        }
        
        //get election
        var election = _electionRepository.GetElectionByYear(year);
        if (election is null)
        {
            return -1;
        }
        
        //get election round
        var electionRound = _electionRoundRepository.GetElectionRoundByElectionIdAndTour(election.ElectionId, tour);
        return electionRound?.TotalVotes ?? 0;
    }


    public IEnumerable<RoundCandidateDto> GetCandidatesForYearAndTour(int year, int tour)
    {
        if (tour != 1 && tour != 2)
        {
            return null;
        }
        
        //get election
        var election = _electionRepository.GetElectionByYear(year);
        if (election is null)
        {
            return null;
        }
        
        //get election round
        var electionRound = _electionRoundRepository.GetElectionRoundByElectionIdAndTour(election.ElectionId, tour);

        if (electionRound is null)
        {
            return null;
        }
        
        var finalList = new List<RoundCandidateDto>();
        
        
        var electionRoundCandidates = _electionRoundRepository.GetRoundCandidates(electionRound.ElectionRoundId);
        
        foreach (var roundCandidate in electionRoundCandidates)
        {
            Console.WriteLine("Found round candidate: " + roundCandidate.RoundCandidateId);
            //iau informatiile din candidate table pe baza fk
            //pun tot intr un RoundCandidateDto
            var matchingCandidate = _candidateRepository.GetCandidate(roundCandidate.CandidateId);
            if (matchingCandidate is null)
            {
                continue;
            }
            var roundCandidateDto = new RoundCandidateDto
            {
                Votes = roundCandidate.Votes,
                Bio = matchingCandidate.Bio,
                ImageUrl = matchingCandidate.ImageUrl,
                Name = matchingCandidate.Name,
                Party = matchingCandidate.Party,
                Percentage = roundCandidate.Percentage
            };
            finalList.Add(roundCandidateDto);
        }
        return finalList;
    }
}