using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class JobListRepository : RepositoryBase<JobList>, IJobListRepository
    {
        public JobListRepository(RepositoryContext context) : base(context) { }

        public IQueryable<JobList> GetAllJobLists(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<JobList> GetAllJobListsWithDepartment(bool trackChanges) =>
            FindAll(trackChanges)
                .Include(j => j.Department)
                .Include(j => j.Job); // <-- Job bilgisi dahil edildi

        public IQueryable<JobList> GetJobListById(int id, bool trackChanges) =>
            FindByCondition(jl => jl.JobListId == id, trackChanges);

        public IQueryable<JobList> GetJobListByIdWithDepartment(int id, bool trackChanges) =>
            FindByCondition(jl => jl.JobListId == id, trackChanges)
                .Include(j => j.Department)
                .Include(j => j.Job); // <-- Job bilgisi dahil edildi

        public void CreateJobList(JobList jobList) => Create(jobList);
        public void UpdateJobList(JobList jobList) => Update(jobList);
        public void DeleteJobList(JobList jobList) => Delete(jobList);
    }
}
