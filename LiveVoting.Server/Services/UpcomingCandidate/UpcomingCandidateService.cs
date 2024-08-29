using LiveVoting.Server.Repositories.UpcomingCandidate;
using LiveVoting.Shared.Dtos;

namespace LiveVoting.Server.Services.UpcomingCandidate;

public class UpcomingCandidateService : IUpcomingCandidateService
{
    
    private readonly IUpcomingCandidateRepository _upcomingCandidateRepository;

    public UpcomingCandidateService(IUpcomingCandidateRepository upcomingCandidateRepository)
    {
        _upcomingCandidateRepository = upcomingCandidateRepository;
    }
    
    
    public ICollection<CandidateDto> GetCandidates()
    {
        var foundCandidates = _upcomingCandidateRepository.GetAllUpcomingCandidates();
        var dtos = new List<CandidateDto>();
        foreach (var candidate in foundCandidates)
        {
            dtos.Add(new CandidateDto
            {
                Bio = candidate.Bio,
                Name = candidate.Name,
                ImageUrl = candidate.ImageUrl,
                Party = candidate.Party
            });
        }

        return dtos;
    }

    public CandidateDto? GetCandidate(string name)
    {
        var foundCandidate = _upcomingCandidateRepository.GetUpcomingCandidateByName(name);
        if (foundCandidate is null)
        {
            return null;
        }
        var dtoCandidate = new CandidateDto
        {
            Bio = foundCandidate.Bio,
            Name = foundCandidate.Name,
            Party = foundCandidate.Party,
            ImageUrl = foundCandidate.ImageUrl,
        };
        return dtoCandidate;
    }

    public CandidateDto? GetCandidate(int candidateId)
    {
        var foundCandidate = _upcomingCandidateRepository.GetUpcomingCandidateById(candidateId);
        if (foundCandidate is null)
        {
            return null;
        }
        var dtoCandidate = new CandidateDto
        {
            Bio = foundCandidate.Bio,
            Name = foundCandidate.Name,
            Party = foundCandidate.Party,
            ImageUrl = foundCandidate.ImageUrl,
        };
        return dtoCandidate;
    }

    public Models.UpcomingCandidate? GetCandidateByImageUrl(string imageUrl)
    {
        return _upcomingCandidateRepository.GetUpcomingCandidateByImageUrl(imageUrl);
    }
}