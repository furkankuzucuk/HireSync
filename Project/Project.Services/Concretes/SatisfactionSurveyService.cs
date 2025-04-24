using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.SatisfactionSurvey;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class SatisfactionSurveyService : ISatisfactionSurveyService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public SatisfactionSurveyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<SatisfactionSurveyDto> CreateSatisfactionSurvey(SatisfactionSurveyInsertDto satisfactionSurvey)
        {
            var entity = mapper.Map<SatisfactionSurvey>(satisfactionSurvey);
            repositoryManager.SatisfactionSurveyRepository.CreateSatisfactionSurvey(entity);
            await repositoryManager.Save();
            return mapper.Map<SatisfactionSurveyDto>(entity);
        }

        public async Task DeleteSatisfactionSurvey(int id, bool trackChanges)
        {
            var entity = await repositoryManager.SatisfactionSurveyRepository.GetSatisfactionSurveyById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<SatisfactionSurvey>(id);

            repositoryManager.SatisfactionSurveyRepository.DeleteSatisfactionSurvey(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<SatisfactionSurveyDto>> GetAllSatisfactionSurveys(bool trackChanges)
        {
            var surveys = await repositoryManager.SatisfactionSurveyRepository.GetAllSatisfactionSurveys(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<SatisfactionSurveyDto>>(surveys);
        }

        public async Task<SatisfactionSurveyDto> GetSatisfactionSurveyById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.SatisfactionSurveyRepository.GetSatisfactionSurveyById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<SatisfactionSurvey>(id);

            return mapper.Map<SatisfactionSurveyDto>(entity);
        }

        public async Task UpdateSatisfactionSurvey(int id, SatisfactionSurveyUpdateDto satisfactionSurvey, bool trackChanges)
        {
            var entity = await repositoryManager.SatisfactionSurveyRepository.GetSatisfactionSurveyById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<SatisfactionSurvey>(id);

            mapper.Map(satisfactionSurvey, entity);
            await repositoryManager.Save();
        }
    }
}
