using Project.Entities.DataTransferObjects.Candidate;

namespace Project.Services.Contracts
{
    public interface ICandidateService
    {
        Task<CandidateDto> CreateCandidate(CandidateDtoInsertion candidateDto);
        Task<CandidateDto> GetCandidateById(int id,bool trackChanges);
        Task UpdateCandidate(int id, CandidateDtoUpdate candidateDto);
        Task<CandidateDto> AuthenticateCandidate(CandidateAuthenticationDto candidateAuthDto);
    }
}
