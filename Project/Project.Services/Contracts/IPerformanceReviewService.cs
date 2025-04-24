using Project.Entities;
using Project.Entities.DataTransferObjects.PerformanceReview;

namespace Project.Services.Contracts
{
    public interface IPerformanceReviewService
    {
        Task<IEnumerable<PerformanceReviewDto>> GetAllPerformanceReviews(bool trackChanges);
        Task<PerformanceReviewDto> GetPerformanceReviewById(int id, bool trackChanges);
        Task<PerformanceReviewDto> CreatePerformanceReview(PerformanceReviewInsertDto performanceReview);
        Task UpdatePerformanceReview(int id, PerformanceReviewUpdateDto performanceReview, bool trackChanges);
        Task DeletePerformanceReview(int id, bool trackChanges);
    }
}
