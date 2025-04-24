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
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public PerformanceReviewService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<PerformanceReviewDto> CreatePerformanceReview(PerformanceReviewInsertDto performanceReview)
        {
            var entity = mapper.Map<PerformanceReview>(performanceReview);
            repositoryManager.PerformanceReviewRepository.CreatePerformanceReview(entity);
            await repositoryManager.Save();
            return mapper.Map<PerformanceReviewDto>(entity);
        }

        public async Task DeletePerformanceReview(int id, bool trackChanges)
        {
            var entity = await repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            repositoryManager.PerformanceReviewRepository.DeletePerformanceReview(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<PerformanceReviewDto>> GetAllPerformanceReviews(bool trackChanges)
        {
            var reviews = await repositoryManager.PerformanceReviewRepository.GetAllPerformanceReviews(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<PerformanceReviewDto>>(reviews);
        }

        public async Task<PerformanceReviewDto> GetPerformanceReviewById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            return mapper.Map<PerformanceReviewDto>(entity);
        }

        public async Task UpdatePerformanceReview(int id, PerformanceReviewUpdateDto performanceReview, bool trackChanges)
        {
            var entity = await repositoryManager.PerformanceReviewRepository.GetPerformanceReviewById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<PerformanceReview>(id);

            mapper.Map(performanceReview, entity);
            await repositoryManager.Save();
        }
    }
}
