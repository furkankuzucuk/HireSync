using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
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
            return FindByCondition(j => j.JobId == id, trackChanges);
        }

        public void UpdateJob(Job job)
        {
            Update(job);
        }

        public IQueryable<Job> FindByCondition(Expression<Func<Job, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? _context.Jobs.Where(expression)
                : _context.Jobs.Where(expression).AsNoTracking();
        }

        // ✅ Eksik olan metod eklendi: DepartmanId'ye göre iş pozisyonları getir
        public IQueryable<Job> GetJobsByDepartmentId(int departmentId, bool trackChanges)
        {
            return FindByCondition(j => j.DepartmentId == departmentId, trackChanges);
        }
    }
}
