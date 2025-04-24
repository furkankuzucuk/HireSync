using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface IJobApplicationRepository : IRepositoryBase<JobApplication>
    {
        IQueryable<JobApplication> GetAllJobApplications(bool trackChanges);
        IQueryable<JobApplication> GetJobApplicationById(int id, bool trackChanges);
        void CreateJobApplication(JobApplication jobApplication);
        void UpdateJobApplication(JobApplication jobApplication);
        void DeleteJobApplication(JobApplication jobApplication);
    }
}
