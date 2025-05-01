using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface IJobListRepository : IRepositoryBase<JobList>
    {
        IQueryable<JobList> GetAllJobLists(bool trackChanges);
        IQueryable<JobList> GetAllJobListsWithDepartment(bool trackChanges);
        IQueryable<JobList> GetJobListById(int id, bool trackChanges);
        IQueryable<JobList> GetJobListByIdWithDepartment(int id, bool trackChanges);
        void CreateJobList(JobList jobList);
        void UpdateJobList(JobList jobList);
        void DeleteJobList(JobList jobList);
    }
}
