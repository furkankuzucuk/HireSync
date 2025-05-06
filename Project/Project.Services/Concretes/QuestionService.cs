using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public QuestionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        // Yeni bir soru oluştur
        public async Task<QuestionDto> CreateQuestion(QuestionInsertDto questionDto)
        {
            if (questionDto == null)
            {
                throw new ArgumentNullException(nameof(questionDto), "Question data is null.");
            }

            var questionEntity = mapper.Map<Question>(questionDto);
            repositoryManager.QuestionRepository.CreateQuestion(questionEntity);
            await repositoryManager.Save();

            return mapper.Map<QuestionDto>(questionEntity);
        }

        // Soru silme işlemi
        public async Task DeleteQuestion(int id, bool trackChanges)
        {
            var questionEntity = await repositoryManager.QuestionRepository
                .GetQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (questionEntity == null)
            {
                throw new EntityNotFoundException<Question>(id);
            }

            repositoryManager.QuestionRepository.DeleteQuestion(questionEntity);
            await repositoryManager.Save();
        }

        // Tüm soruları getir
        public async Task<IEnumerable<QuestionDto>> GetAllQuestions(bool trackChanges)
        {
            var questions = await repositoryManager.QuestionRepository
                .GetAllQuestions(trackChanges)
                .ToListAsync();

            return mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        // ID ile soru getirme
        public async Task<QuestionDto> GetQuestionById(int id, bool trackChanges)
        {
            var questionEntity = await repositoryManager.QuestionRepository
                .GetQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (questionEntity == null)
            {
                throw new EntityNotFoundException<Question>(id);
            }

            return mapper.Map<QuestionDto>(questionEntity);
        }

        // Soru güncelleme işlemi
        public async Task UpdateQuestion(int id, QuestionUpdateDto questionDto, bool trackChanges)
        {
            var questionEntity = await repositoryManager.QuestionRepository
                .GetQuestionById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (questionEntity == null)
            {
                throw new EntityNotFoundException<Question>(id);
            }

            // Mapping the updated question data to the existing entity
            mapper.Map(questionDto, questionEntity);

            await repositoryManager.Save();
        }
    }
}
