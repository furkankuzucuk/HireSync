using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class JobApplicationRepository : RepositoryBase<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(RepositoryContext context) : base(context) { }

        public IQueryable<JobApplication> GetAllJobApplications(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<JobApplication> GetJobApplicationById(int id, bool trackChanges) =>
            FindByCondition(ja => ja.JobApplicationId == id, trackChanges);

        public void CreateJobApplication(JobApplication jobApplication) =>
            Create(jobApplication);

        public void UpdateJobApplication(JobApplication jobApplication) =>
            Update(jobApplication);

        public void DeleteJobApplication(JobApplication jobApplication) =>
            Delete(jobApplication);
    }
}
