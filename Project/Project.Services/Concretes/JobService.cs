
using AutoMapper;
using Project.Entities;
using Project.Entities.DataTransferObjects.Job;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class JobService : IJobService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public JobService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<JobDto> CreateJob(JobInsertDto jobDto)
    {
        var jobEntity = mapper.Map<Job>(jobDto);
        repositoryManager.JobRepository.CreateJob(jobEntity);
        await repositoryManager.Save();
        return mapper.Map<JobDto>(jobEntity);
    }

    public Task DeleteJob(int id, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JobDto>> GetAllJobs(bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<JobDto> GetJobById(int id, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task UpdateJob(int id, JobUpdateDto jobDto, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}