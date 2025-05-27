using Project.Entities;
using Project.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
namespace Project.Repository.Concretes
{
    public class PerformanceReviewRepository : RepositoryBase<PerformanceReview>, IPerformanceReviewRepository
    {
        public PerformanceReviewRepository(RepositoryContext context) : base(context) { }

        public IQueryable<PerformanceReview> GetAllPerformanceReviews(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<PerformanceReview> GetPerformanceReviewById(int id, bool trackChanges) =>
            FindByCondition(pr => pr.PerformanceReviewId == id, trackChanges);

        public void CreatePerformanceReview(PerformanceReview performanceReview) =>
            Create(performanceReview);

        public void UpdatePerformanceReview(PerformanceReview performanceReview) =>
            Update(performanceReview);

        public void DeletePerformanceReview(PerformanceReview performanceReview) =>
            Delete(performanceReview);

        public IQueryable<PerformanceReview> GetReviewByUserId(int userId, bool trackChanges)
        {
            return _context.PerformanceReviews
                .Include(r => r.UserExam)
                .Where(r => r.UserExam.UserId == userId);
        }

    }
}
