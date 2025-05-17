using Project.Entities.DataTransferObjects.JobList;

namespace Project.Services.Contracts
{
    public interface IJobListService
    {
        Task<IEnumerable<JobListDto>> GetAllJobLists(bool trackChanges);
        Task<JobListDto> GetJobListById(int id, bool trackChanges);
        Task<JobListDto> CreateJobList(JobListInsertDto jobList);
        Task UpdateJobList(int id, JobListUpdateDto jobList, bool trackChanges);
        Task DeleteJobList(int id, bool trackChanges);
    }
}
