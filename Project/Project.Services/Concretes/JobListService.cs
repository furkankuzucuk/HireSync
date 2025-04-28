using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.JobList;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class JobListService : IJobListService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public JobListService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<JobListDto> CreateJobList(JobListInsertDto jobListDto)
        {
            if (jobListDto == null)
                throw new ArgumentNullException(nameof(jobListDto), "İlan bilgileri boş olamaz.");

            var entity = mapper.Map<JobList>(jobListDto);

            // CreateDate'yi burada otomatik ayarla
            entity.CreateDate = DateTime.Now;

            repositoryManager.JobListRepository.CreateJobList(entity);
            await repositoryManager.Save();

            return mapper.Map<JobListDto>(entity);
        }

        public async Task DeleteJobList(int id, bool trackChanges)
        {
            var entity = await repositoryManager.JobListRepository
                .GetJobListById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<JobList>(id);

            repositoryManager.JobListRepository.DeleteJobList(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<JobListDto>> GetAllJobLists(bool trackChanges)
        {
            var jobLists = await repositoryManager.JobListRepository
                .GetAllJobLists(trackChanges)
                .ToListAsync();

            return mapper.Map<IEnumerable<JobListDto>>(jobLists);
        }

        public async Task<JobListDto> GetJobListById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.JobListRepository
                .GetJobListById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<JobList>(id);

            return mapper.Map<JobListDto>(entity);
        }

        public async Task UpdateJobList(int id, JobListUpdateDto jobListDto, bool trackChanges)
        {
            var entity = await repositoryManager.JobListRepository
                .GetJobListById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<JobList>(id);

            mapper.Map(jobListDto, entity);

            await repositoryManager.Save();
        }
    }
}
