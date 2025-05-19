using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;
using System.Linq.Expressions;

namespace Project.Repository.Concretes
{
    public class JobApplicationRepository : RepositoryBase<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(RepositoryContext context) : base(context) { }

        public IQueryable<JobApplication> GetAllJobApplications(bool trackChanges) =>
    FindAll(trackChanges)
        .Include(ja => ja.JobList)
            .ThenInclude(jl => jl.Job)
        .Include(ja => ja.JobList)
            .ThenInclude(jl => jl.Department);


        public IQueryable<JobApplication> GetJobApplicationById(int id, bool trackChanges) =>
            FindByCondition(ja => ja.JobApplicationId == id, trackChanges)
                .Include(ja => ja.JobList)
                    .ThenInclude(jl => jl.Job)
                .Include(ja => ja.JobList)
                    .ThenInclude(jl => jl.Department);

        public IQueryable<JobApplication> FindByCondition(Expression<Func<JobApplication, bool>> expression, bool trackChanges) =>
            base.FindByCondition(expression, trackChanges)
                .Include(ja => ja.JobList)
                    .ThenInclude(jl => jl.Job)
                .Include(ja => ja.JobList)
                    .ThenInclude(jl => jl.Department);

        public void CreateJobApplication(JobApplication jobApplication) =>
            Create(jobApplication);

        public void UpdateJobApplication(JobApplication jobApplication) =>
            Update(jobApplication);

        public void DeleteJobApplication(JobApplication jobApplication) =>
            Delete(jobApplication);
    }
}
