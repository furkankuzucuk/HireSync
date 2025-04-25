using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Candidate;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CandidateService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<CandidateDto> CreateCandidate(CandidateDtoInsertion candidateDto)
        {
            var candidateEntity = _mapper.Map<Candidate>(candidateDto);
            _repositoryManager.CandidateRepository.CreateCandidate(candidateEntity);
            await _repositoryManager.Save();
            return _mapper.Map<CandidateDto>(candidateEntity);
        }

        public async Task<CandidateDto> GetCandidateById(int id,bool trackChanges)
        {
            var candidateEntity = await _repositoryManager.CandidateRepository.GetCandidateById(id, trackChanges).FirstOrDefaultAsync();
            if (candidateEntity == null)
            {
                throw new EntityNotFoundException<Candidate>(id);
            }

            return _mapper.Map<CandidateDto>(candidateEntity);
        }

        public async Task UpdateCandidate(int id, CandidateDtoUpdate candidateDto)
        {
            var candidateEntity = await _repositoryManager.CandidateRepository.GetCandidateById(id, false).FirstOrDefaultAsync();
            if (candidateEntity == null)
            {
                throw new EntityNotFoundException<Candidate>(id);
            }

            _mapper.Map(candidateDto, candidateEntity);
            await _repositoryManager.Save();
        }

        public async Task<CandidateDto> AuthenticateCandidate(CandidateAuthenticationDto candidateAuthDto)
        {
            var candidate = await _repositoryManager.CandidateRepository
                .FindByCondition(c => c.Mail == candidateAuthDto.Mail && c.Password == candidateAuthDto.Password, false)
                .FirstOrDefaultAsync();

            if (candidate == null)
                return null;

            return _mapper.Map<CandidateDto>(candidate);
        }
    }
}
