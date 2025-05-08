using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.SurveyAnswer;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class SurveyAnswerService : ISurveyAnswerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SurveyAnswerService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<SurveyAnswerDto> CreateSurveyAnswer(SurveyAnswerInsertDto answerDto)
        {
            var answerEntity = _mapper.Map<SurveyAnswer>(answerDto);
            _repositoryManager.SurveyAnswerRepository.CreateSurveyAnswer(answerEntity);
            await _repositoryManager.Save();
            return _mapper.Map<SurveyAnswerDto>(answerEntity);
        }

        public async Task<IEnumerable<SurveyAnswerDto>> GetAllSurveyAnswers(bool trackChanges)
        {
            var answers = await _repositoryManager.SurveyAnswerRepository
                .GetAllSurveyAnswers(trackChanges)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SurveyAnswerDto>>(answers);
        }

        public async Task<SurveyAnswerDto> GetSurveyAnswerById(int id, bool trackChanges)
        {
            var answer = await _repositoryManager.SurveyAnswerRepository
                .GetSurveyAnswerById(id,trackChanges)
                .FirstOrDefaultAsync();

            if (answer == null)
                throw new EntityNotFoundException<SurveyAnswer>(id);

            return _mapper.Map<SurveyAnswerDto>(answer);
        }

        public async Task<IEnumerable<SurveyAnswerDto>> GetSurveyAnswersByQuestionId(int questionId, bool trackChanges)
        {
            var answers = await _repositoryManager.SurveyAnswerRepository
                .GetSurveyAnswersByQuestionId(questionId, trackChanges)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SurveyAnswerDto>>(answers);
        }

        public async Task DeleteSurveyAnswer(int id, bool trackChanges)
        {
            var answer = await _repositoryManager.SurveyAnswerRepository
                .GetSurveyAnswersByQuestionId(id,trackChanges)
                .FirstOrDefaultAsync();

            if (answer == null)
                throw new EntityNotFoundException<SurveyAnswer>(id);

            _repositoryManager.SurveyAnswerRepository.DeleteSurveyAnswer(answer);
            await _repositoryManager.Save();
        }

        public async Task UpdateSurveyAnswer(int id, SurveyAnswerUpdateDto surveyDto, bool trackChanges)
        {
                var answerEntity = await _repositoryManager.SurveyAnswerRepository
                .GetSurveyAnswerById(id, trackChanges)
                .FirstOrDefaultAsync();

            // Eğer cevap bulunamazsa, bir hata fırlatıyoruz
            if (answerEntity == null)
            {
                throw new EntityNotFoundException<SurveyAnswer>(id);
            }

            // Mapping: gelen DTO ile veritabanındaki entity'yi güncelliyoruz
            _mapper.Map(surveyDto, answerEntity);

            // Değişiklikleri kaydediyoruz
            await _repositoryManager.Save();
        }
    }
}
