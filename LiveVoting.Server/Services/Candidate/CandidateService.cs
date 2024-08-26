using LiveVoting.Server.Repositories.Candidate;
using LiveVoting.Shared.Dtos;

namespace LiveVoting.Server.Services.Candidate;

public class CandidateService : ICandidateService
{
    
    private readonly ICandidateRepository _candidateRepository;

    public CandidateService(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }
    
    public async Task<ICollection<CandidateDto>> GetCandidatesAsync()
    {
        var candidates= await _candidateRepository.GetCandidatesAsync();
        ICollection<CandidateDto> dtos = new List<CandidateDto>();
        foreach (var candidate in candidates)
        {
            dtos.Add(new CandidateDto
            {
                Bio = candidate.Bio,
                Name = candidate.Name,
                Party = candidate.Party,
                ImageUrl = candidate.ImageUrl
            });
        }

        return dtos;
    }

    public CandidateDto? GetCandidate(int id)
    {
        var candidate =  _candidateRepository.GetCandidate(id);
        if (candidate is null)
        {
            return null;
        }
        return new CandidateDto
        {
            Bio = candidate.Bio,
            ImageUrl = candidate.ImageUrl,
            Name = candidate.Name,
            Party = candidate.Party
        };
    }
}