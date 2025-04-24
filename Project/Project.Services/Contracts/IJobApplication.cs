using Project.Entities;
using Project.Entities.DataTransferObjects.JobApplication;

namespace Project.Services.Contracts
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationDto>> GetAllJobApplications(bool trackChanges);
        Task<JobApplicationDto> GetJobApplicationById(int id, bool trackChanges);
        Task<JobApplicationDto> CreateJobApplication(JobApplicationInsertDto jobApplication);
        Task UpdateJobApplication(int id, JobApplicationUpdateDto jobApplication, bool trackChanges);
        Task DeleteJobApplication(int id, bool trackChanges);
    }
}
