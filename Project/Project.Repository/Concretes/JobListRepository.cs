using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class JobListRepository : RepositoryBase<JobList>, IJobListRepository
    {
        public JobListRepository(RepositoryContext context) : base(context) { }

        public IQueryable<JobList> GetAllJobLists(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<JobList> GetJobListById(int id, bool trackChanges) =>
            FindByCondition(jl => jl.JobListId == id, trackChanges);

        public void CreateJobList(JobList jobList) =>
            Create(jobList);

        public void UpdateJobList(JobList jobList) =>
            Update(jobList);

        public void DeleteJobList(JobList jobList) =>
            Delete(jobList);
    }
}
