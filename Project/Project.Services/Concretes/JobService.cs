using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Job;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
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

        public async Task DeleteJob(int id, bool trackChanges)
        {
            var jobEntity = await repositoryManager.JobRepository.GetJobById(id, trackChanges).FirstOrDefaultAsync();
            if (jobEntity == null)
                throw new EntityNotFoundException<Job>(id);

            repositoryManager.JobRepository.DeleteJob(jobEntity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<JobDto>> GetAllJobs(bool trackChanges)
        {
            var jobs = await repositoryManager.JobRepository.GetAllJobs(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<JobDto>>(jobs);
        }

        public async Task<JobDto> GetJobById(int id, bool trackChanges)
        {
            var jobEntity = await repositoryManager.JobRepository.GetJobById(id, trackChanges).FirstOrDefaultAsync();
            if (jobEntity == null)
                throw new EntityNotFoundException<Job>(id);

            return mapper.Map<JobDto>(jobEntity);
        }

        public async Task UpdateJob(int id, JobUpdateDto jobDto, bool trackChanges)
        {
            var jobEntity = await repositoryManager.JobRepository.GetJobById(id, trackChanges).FirstOrDefaultAsync();
            if (jobEntity == null)
                throw new EntityNotFoundException<Job>(id);

            mapper.Map(jobDto, jobEntity);
            await repositoryManager.Save();
        }
    }
}
