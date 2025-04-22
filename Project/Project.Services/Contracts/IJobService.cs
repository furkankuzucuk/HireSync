
using Project.Entities.DataTransferObjects.Job;

namespace Project.Services.Contracts;

public interface IJobService
{
    Task<IEnumerable<JobDto>> GetAllJobs(bool trackChanges);
    Task<JobDto> GetJobById(int id , bool trackChanges);
    Task<JobDto> CreateJob(JobInsertDto jobDto);
    Task UpdateJob(int id,JobUpdateDto jobDto,bool trackChanges);
    Task DeleteJob(int id , bool trackChanges);
}