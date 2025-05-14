using System.Linq.Expressions;
using Project.Entities;
namespace Project.Repository.Contracts;

public interface IJobRepository : IRepositoryBase<Job>
{
    IQueryable<Job> GetAllJobs(bool trackChanges);
    IQueryable<Job> GetJobById(int id , bool trakChanges);
    void CreateJob(Job job);
    void UpdateJob(Job job);
    void DeleteJob(Job job);
      IQueryable<Job> FindByCondition(Expression<Func<Job, bool>> expression, bool trackChanges);
}

