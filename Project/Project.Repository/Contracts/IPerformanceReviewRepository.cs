using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface IPerformanceReviewRepository : IRepositoryBase<PerformanceReview>
    {
        IQueryable<PerformanceReview> GetAllPerformanceReviews(bool trackChanges);
        IQueryable<PerformanceReview> GetPerformanceReviewById(int id, bool trackChanges);
        IQueryable<PerformanceReview> GetReviewByUserId(int userId, bool trackChanges);
        void CreatePerformanceReview(PerformanceReview performanceReview);
        void UpdatePerformanceReview(PerformanceReview performanceReview);
        void DeletePerformanceReview(PerformanceReview performanceReview);
    }
}
