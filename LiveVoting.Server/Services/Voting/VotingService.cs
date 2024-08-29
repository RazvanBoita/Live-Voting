using LiveVoting.Server.Models;
using LiveVoting.Server.Repositories.UpcomingCandidate;
using LiveVoting.Server.Repositories.UpcomingVote;
using LiveVoting.Server.Repositories.User;

namespace LiveVoting.Server.Services.Voting;

public class VotingService : IVotingService
{
    private readonly IUpcomingVoteRepository _upcomingVoteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUpcomingCandidateRepository _upcomingCandidateRepository;
    
    public VotingService(IUpcomingVoteRepository upcomingVoteRepository, IUserRepository userRepository, IUpcomingCandidateRepository upcomingCandidateRepository)
    {
        _upcomingVoteRepository = upcomingVoteRepository;
        _userRepository = userRepository;
        _upcomingCandidateRepository = upcomingCandidateRepository;
    }
    
    
    public (bool, string) AddVote(Models.UpcomingCandidate? upcomingCandidate, Guid voterId)
    {
        
        var edgeCasesResult = CheckEdgeCases(upcomingCandidate, voterId);
        if (!edgeCasesResult.Item1)
        {
            return (false, edgeCasesResult.Item2);
        }
        
        //check if vote exists already for voter id
        if (_upcomingVoteRepository.UserVotedAlready(voterId))
        {
            return (false, "User already voted");
        }
        
        //totul e ok
        var upcomingVote = new UpcomingVote
        {
            UserId = voterId,
            UpcomingCandidateId = upcomingCandidate.UpcomingCandidateId
        };
        var result = _upcomingVoteRepository.AddVote(upcomingVote);
        if (!result)
        {
            return (false, "Failed to add vote");
        }
        return (true, "Vote added successfully");
    }

    public (bool, string) RemoveVote(Models.UpcomingCandidate? upcomingCandidate, Guid voterId)
    {
        
        
        
        var edgeCasesResult = CheckEdgeCases(upcomingCandidate, voterId);
        if (!edgeCasesResult.Item1)
        {
            return (false, edgeCasesResult.Item2);
        }
        
        //TODO Verifica si daca da remove la cine trebuie
        //O potentiala eroare e sa dau remove dupa guid, dar el poate incearca sa dea remove la altcineva, putin posibil sa se intample pe frontend, dar bine de acoperit
        if (!_upcomingVoteRepository.ExistsPairOfUserIdAndUpcomingCandidate(voterId, upcomingCandidate!))
        {
            return (false, "No corresponding vote found");
        }
        
        var searchedVote = _upcomingVoteRepository.GetVoteByGuid(voterId);
        if (searchedVote is null)
        {
            return (false, "No vote found");
        }
        
        var result = _upcomingVoteRepository.RemoveVote(searchedVote);
        if (!result)
        {
            return (false, "Failed to remove vote");
        }
        return (true, "Vote removed successfully");
    }

    public (bool, string) CheckEdgeCases(Models.UpcomingCandidate? upcomingCandidate, Guid voterId)
    {
        //check upcoming candidate is ok
        if (upcomingCandidate is null)
        {
            return (false, "Candidate not found");
        }
        
        //check voter exists
        if (!_userRepository.UserExistsById(voterId))
        {
            return (false, "User not found");
        }

        return (true, "ok");
    }

    public Models.UpcomingCandidate? GetVotedCandidateForUserId(Guid guid)
    {
        //go into the upcomingvotes repo and get the vote
        var voteFound = _upcomingVoteRepository.GetVoteByGuid(guid);
        if (voteFound is null)
        {
            return null;
        }
        
        //get the candidate based on the candidate id
        return _upcomingCandidateRepository.GetUpcomingCandidateById(voteFound.UpcomingCandidateId);
    }

    public int GetVotesCountForCandidate(Models.UpcomingCandidate? candidate)
    {
        if (candidate is null)
        {
            return -1;
        }
        
        return _upcomingVoteRepository.GetVotesCountForUpcomingCandidateId(candidate.UpcomingCandidateId);
    }
}