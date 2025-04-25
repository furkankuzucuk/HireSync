using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public JobApplicationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<JobApplicationDto> CreateJobApplication(JobApplicationInsertDto jobApplication)
        {
            var jobApplicationEntity = mapper.Map<JobApplication>(jobApplication);
            repositoryManager.JobApplicationRepository.CreateJobApplication(jobApplicationEntity);
            await repositoryManager.Save();
            return mapper.Map<JobApplicationDto>(jobApplicationEntity);
        }

        public async Task DeleteJobApplication(int id, bool trackChanges)
        {
            var jobApplicationEntity = await repositoryManager.JobApplicationRepository.GetJobApplicationById(id, trackChanges).FirstOrDefaultAsync();
            if (jobApplicationEntity == null)
                throw new EntityNotFoundException<JobApplication>(id);

            repositoryManager.JobApplicationRepository.DeleteJobApplication(jobApplicationEntity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllJobApplications(bool trackChanges)
        {
            var jobApplications = await repositoryManager.JobApplicationRepository.GetAllJobApplications(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<JobApplicationDto>>(jobApplications);
        }

        public async Task<JobApplicationDto> GetJobApplicationById(int id, bool trackChanges)
        {
            var jobApplicationEntity = await repositoryManager.JobApplicationRepository.GetJobApplicationById(id, trackChanges).FirstOrDefaultAsync();
            if (jobApplicationEntity == null)
                throw new EntityNotFoundException<JobApplication>(id);

            return mapper.Map<JobApplicationDto>(jobApplicationEntity);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByCandidateId(int candidateId)
        {
            var applications = await repositoryManager.JobApplicationRepository
            .FindByCondition(a => a.CandidateId == candidateId, false)
            .ToListAsync();
            return mapper.Map<IEnumerable<JobApplicationDto>>(applications);
        }


        public async Task UpdateJobApplication(int id, JobApplicationUpdateDto jobApplication, bool trackChanges)
        {
            var jobApplicationEntity = await repositoryManager.JobApplicationRepository.GetJobApplicationById(id, trackChanges).FirstOrDefaultAsync();
            if (jobApplicationEntity == null)
                throw new EntityNotFoundException<JobApplication>(id);

            mapper.Map(jobApplication, jobApplicationEntity);
            await repositoryManager.Save();
        }
    }
}
