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
            var entity = await _repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            _repositoryManager.PerformanceReviewRepository.DeletePerformanceReview(entity);
            await _repositoryManager.Save();
        }

        public async Task<PerformanceReviewDto> GeneratePerformanceReviewForUser(int userId)
        {
             var userExams = await _repositoryManager.UserExamRepository
            .FindByCondition(ue => ue.UserId == userId, trackChanges: false)
            .ToListAsync();

            if (userExams == null || !userExams.Any())
            {
                throw new Exception($"User with id {userId} has no exams to review.");
            }

    // 2. Ortalama puanı hesapla
            var averageScore = userExams.Average(ue => ue.Score);

            // 3. Ortalama skora göre performans notu belirle (örnek basit bir mantıkla)
            byte performanceRate;
            if (averageScore >= 90) performanceRate = 5;
            else if (averageScore >= 75) performanceRate = 4;
            else if (averageScore >= 60) performanceRate = 3;
            else if (averageScore >= 50) performanceRate = 2;
            else performanceRate = 1;

    // 4. Yeni PerformanceReview oluştur
            var performanceReview = new PerformanceReview
            {
                UserId = userId,
                PerformanceRate = performanceRate,
                ReviewSummary = $"User has an average score of {averageScore:F2}",
                ReviewDate = DateTime.UtcNow,
                //ExamId = userExams.OrderByDescending(e => e.ExamDate).First().ExamId // Son girdiği sınavı referans alıyoruz
            };

            _repositoryManager.PerformanceReviewRepository.CreatePerformanceReview(performanceReview);
            await _repositoryManager.Save();

    // 5. Dto'ya map'leyip dön
            return mapper.Map<PerformanceReviewDto>(performanceReview);
        }

        public async Task<IEnumerable<PerformanceReviewDto>> GetAllPerformanceReviews(bool trackChanges)
        {
            var reviews = await _repositoryManager.PerformanceReviewRepository.GetAllPerformanceReviews(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<PerformanceReviewDto>>(reviews);
        }

        public async Task<PerformanceReviewDto> GetPerformanceReviewById(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
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
            var entity = await _repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            mapper.Map(performanceReview, entity);
            await _repositoryManager.Save();
        }
    }
}
