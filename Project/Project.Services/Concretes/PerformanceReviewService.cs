using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.PerformanceReview;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper mapper;

        public PerformanceReviewService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<PerformanceReviewDto> CreatePerformanceReview(PerformanceReviewInsertDto performanceReview)
        {
            var entity = mapper.Map<PerformanceReview>(performanceReview);
            _repositoryManager.PerformanceReviewRepository.CreatePerformanceReview(entity);
            await _repositoryManager.Save();
            return mapper.Map<PerformanceReviewDto>(entity);
        }

        public async Task DeletePerformanceReview(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.PerformanceReviewRepository
                .GetPerformanceReviewById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            _repositoryManager.PerformanceReviewRepository.DeletePerformanceReview(entity);
            await _repositoryManager.Save();
        }

        public async Task<PerformanceReviewDto> GeneratePerformanceReviewForUser(int userId)
        {
            var userExams = await _repositoryManager.UserExamRepository
                .FindByCondition(ue => ue.UserId == userId, trackChanges: false)
                .Include(ue => ue.Exam) // Exam.ExamDate için gerekli
                .ToListAsync();

            if (userExams == null || !userExams.Any())
                throw new Exception($"User with id {userId} has no exams to review.");

            var averageScore = userExams.Average(ue => ue.Score);

            byte performanceRate = averageScore switch
            {
                >= 90 => 5,
                >= 75 => 4,
                >= 60 => 3,
                >= 50 => 2,
                _ => 1
            };

            var latestUserExam = userExams
                .OrderByDescending(e => e.Exam.ExamDate)
                .FirstOrDefault();

            if (latestUserExam == null)
                throw new Exception("Could not determine the latest user exam.");

            var performanceReview = new PerformanceReview
            {
                UserExamId = latestUserExam.UserExamId,
                PerformanceRate = performanceRate,
                ReviewSummary = $"User has an average score of {averageScore:F2}",
                ReviewDate = DateTime.UtcNow,
                AverageScore = averageScore
            };

            _repositoryManager.PerformanceReviewRepository.CreatePerformanceReview(performanceReview);
            await _repositoryManager.Save();

            return mapper.Map<PerformanceReviewDto>(performanceReview);
        }

        public async Task<IEnumerable<PerformanceReviewDto>> GetAllPerformanceReviews(bool trackChanges)
        {
            var reviews = await _repositoryManager.PerformanceReviewRepository
                .GetAllPerformanceReviews(trackChanges)
                .ToListAsync();

            return mapper.Map<IEnumerable<PerformanceReviewDto>>(reviews);
        }

        public async Task<PerformanceReviewDto> GetPerformanceReviewById(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.PerformanceReviewRepository
                .GetPerformanceReviewById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            return mapper.Map<PerformanceReviewDto>(entity);
        }

        public async Task<IEnumerable<PerformanceReviewDto>> GetReviewsByUserId(int userId, bool trackChanges)
        {
            var reviews = await _repositoryManager.PerformanceReviewRepository
                .GetReviewByUserId(userId, trackChanges)
                .ToListAsync();

            return mapper.Map<IEnumerable<PerformanceReviewDto>>(reviews);
        }

        public async Task UpdatePerformanceReview(int id, PerformanceReviewUpdateDto performanceReview, bool trackChanges)
        {
            var entity = await _repositoryManager.PerformanceReviewRepository
                .GetPerformanceReviewById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            mapper.Map(performanceReview, entity);
            await _repositoryManager.Save();
        }
    }
}
