using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SurveyQuestionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<SurveyQuestionDto> CreateSurveyQuestion(SurveyQuestionInsertDto questionDto)
        {
            var questionEntity = _mapper.Map<SurveyQuestion>(questionDto);
            _repositoryManager.SurveyQuestionRepository.CreateSurveyQuestion(questionEntity);
            await _repositoryManager.Save();
            return _mapper.Map<SurveyQuestionDto>(questionEntity);
        }

        public async Task<IEnumerable<SurveyQuestionDto>> GetAllSurveyQuestions(bool trackChanges)
        {
            var questions = await _repositoryManager.SurveyQuestionRepository.GetAllSurveyQuestion(trackChanges)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SurveyQuestionDto>>(questions);
        }

        public async Task<SurveyQuestionDto> GetSurveyQuestionById(int id, bool trackChanges)
        {
            var question = await _repositoryManager.SurveyQuestionRepository
                .GetSurveyQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (question == null)
                throw new EntityNotFoundException<SurveyQuestion>(id);

            return _mapper.Map<SurveyQuestionDto>(question);
        }

        public async Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestionsBySurveyId(int surveyId, bool trackChanges)
        {
            var questions = await _repositoryManager.SurveyQuestionRepository
                .GetSurveyQuestionsBySurveyId(surveyId, trackChanges)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SurveyQuestionDto>>(questions);
        }

        public async Task DeleteSurveyQuestion(int id, bool trackChanges)
        {
            var question = await _repositoryManager.SurveyQuestionRepository
                .GetSurveyQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (question == null)
                throw new EntityNotFoundException<SurveyQuestion>(id);

            _repositoryManager.SurveyQuestionRepository.DeleteSurveyQuestion(question);
            await _repositoryManager.Save();
        }

        public async Task<IEnumerable<SurveyQuestionResultDto>> GetSurveyResultsGroupedByQuestion(int surveyId)
        {
            var questions = await _repositoryManager.SurveyQuestionRepository
                .FindByCondition(q => q.SatisfactionSurveyId == surveyId, false)
                .Include(q => q.SurveyAnswers)
                .ToListAsync();

            var resultList = questions.Select(q => new SurveyQuestionResultDto
            {
                QuestionText = q.QuestionText,
                AnswerDistribution = q.SurveyAnswers
                    .GroupBy(a => a.Answer)
                    .ToDictionary(g => g.Key, g => g.Count())
            });

            return resultList;
        }


        public async Task UpdateSurveyQuestion(int id,SurveyQuestionUpdateDto surveyQuestionDto,bool trackChanges)
        {
            // Veritabanındaki soruyu buluyoruz
            var questionEntity = await _repositoryManager.SurveyQuestionRepository
                .GetSurveyQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            // Eğer soru bulunmazsa, hata fırlatıyoruz
            if (questionEntity == null)
            {
                throw new EntityNotFoundException<SurveyQuestion>(id);
            }

            // SurveyQuestionDto'dan gelen verilerle entity'yi güncelliyoruz
            _mapper.Map(surveyQuestionDto, questionEntity);

            // Güncellenen soruyu veritabanına kaydediyoruz
            await _repositoryManager.Save();
        }
    }
}
