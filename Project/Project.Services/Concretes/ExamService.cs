using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Exam;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class ExamService : IExamService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public ExamService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<ExamDto> CreateExam(ExamInsertDto exam)
        {
            var examEntity = mapper.Map<Exam>(exam);
            repositoryManager.ExamRepository.CreateExam(examEntity);
            await repositoryManager.Save();
            return mapper.Map<ExamDto>(examEntity);
        }

        public async Task DeleteExam(int id, bool trackChanges)
        {
            var examEntity = await repositoryManager.ExamRepository.GetExamById(id, trackChanges).FirstOrDefaultAsync();
            if (examEntity == null)
                throw new EntityNotFoundException<Exam>(id);

            repositoryManager.ExamRepository.DeleteExam(examEntity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<ExamDto>> GetAllExams(bool trackChanges)
        {
            var exams = await repositoryManager.ExamRepository.GetAllExams(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<ExamDto> GetExamById(int id, bool trackChanges)
        {
            var examEntity = await repositoryManager.ExamRepository.GetExamById(id, trackChanges).FirstOrDefaultAsync();
            if (examEntity == null)
                throw new EntityNotFoundException<Exam>(id);

            return mapper.Map<ExamDto>(examEntity);
        }

        public async Task UpdateExam(int id, ExamUpdateDto examDto, bool trackChanges)
        {
            var examEntity = await repositoryManager.ExamRepository.GetExamById(id, trackChanges).FirstOrDefaultAsync();
            if (examEntity == null)
                throw new EntityNotFoundException<Exam>(id);

            mapper.Map(examDto, examEntity);
            await repositoryManager.Save();
        }
    }
}
