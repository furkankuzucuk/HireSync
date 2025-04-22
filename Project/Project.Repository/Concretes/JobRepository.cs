
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class JobRepository : RepositoryBase<Job>, IJobRepository
{
    public JobRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateJob(Job job)
    {
        Create(job);
    }

    public void DeleteJob(Job job)
    {
        Delete(job);
    }

    public IQueryable<Job> GetAllJobs(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public IQueryable<Job> GetJobById(int id, bool trackChanges)
    {
        return FindByCondition(m => m.JobId == id, trackChanges);
    }

    public void UpdateJob(Job job)
    {
        Update(job);
    }
}