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
                throw new ArgumentNullException(nameof(jobListDto));

            var entity = mapper.Map<JobList>(jobListDto);
            entity.CreateDate = DateTime.Now;

            repositoryManager.JobListRepository.CreateJobList(entity);
            await repositoryManager.Save();

            var jobList = await repositoryManager.JobListRepository
                .GetJobListByIdWithDepartment(entity.JobListId, false)
                .FirstOrDefaultAsync();

            return new JobListDto
            {
                JobListId = jobList.JobListId,
                DepartmentId = jobList.DepartmentId,
                DepartmentName = jobList.Department?.DepartmentName,
                JobId = jobList.JobId,
                JobName = jobList.Job?.JobName,
                Title = jobList.Title,
                Description = jobList.Description,
                CreateDate = jobList.CreateDate
            };
        }

        public async Task<IEnumerable<JobListDto>> GetAllJobLists(bool trackChanges)
        {
            var jobLists = await repositoryManager.JobListRepository
                .GetAllJobListsWithDepartment(trackChanges)
                .ToListAsync();

            return jobLists.Select(j => new JobListDto
            {
                JobListId = j.JobListId,
                DepartmentId = j.DepartmentId,
                DepartmentName = j.Department?.DepartmentName,
                JobId = j.JobId,
                JobName = j.Job?.JobName,
                Title = j.Title,
                Description = j.Description,
                CreateDate = j.CreateDate
            });
        }

        public async Task<JobListDto> GetJobListById(int id, bool trackChanges)
        {
            var jobList = await repositoryManager.JobListRepository
                .GetJobListByIdWithDepartment(id, trackChanges)
                .FirstOrDefaultAsync();

            if (jobList == null)
                throw new EntityNotFoundException<JobList>(id);

            return new JobListDto
            {
                JobListId = jobList.JobListId,
                DepartmentId = jobList.DepartmentId,
                DepartmentName = jobList.Department?.DepartmentName,
                JobId = jobList.JobId,
                JobName = jobList.Job?.JobName,
                Title = jobList.Title,
                Description = jobList.Description,
                CreateDate = jobList.CreateDate
            };
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
    }
}
