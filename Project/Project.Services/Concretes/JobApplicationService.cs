using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;
using Project.Entities.DataTransferObjects.JobList;

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

        public async Task<JobApplicationDto> CreateJobApplication(int userId, JobApplicationInsertDto jobApplication)
        {
            var jobApplicationEntity = mapper.Map<JobApplication>(jobApplication);
            jobApplicationEntity.UserId = userId;
            repositoryManager.JobApplicationRepository.CreateJobApplication(jobApplicationEntity);
            await repositoryManager.Save();

            // JOIN bilgileri çekerek DTO oluştur
            var entity = await repositoryManager.JobApplicationRepository
                .FindByCondition(j => j.JobApplicationId == jobApplicationEntity.JobApplicationId, false)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Department)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Job)
                .FirstOrDefaultAsync();

            return new JobApplicationDto
            {
                JobApplicationId = entity.JobApplicationId,
                JobListId = entity.JobListId,
                UserId = entity.UserId,
                AppDate = entity.AppDate,
                ResumePath = entity.ResumePath,
                Status = entity.Status,
                Title = entity.JobList?.Title,
                DepartmentName = entity.JobList?.Department?.DepartmentName,
                JobName = entity.JobList?.Job?.JobName
            };
        }

        public async Task DeleteJobApplication(int id, bool trackChanges)
        {
            var jobApplicationEntity = await repositoryManager.JobApplicationRepository
                .GetJobApplicationById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (jobApplicationEntity == null)
                throw new EntityNotFoundException<JobApplication>(id);

            repositoryManager.JobApplicationRepository.DeleteJobApplication(jobApplicationEntity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllJobApplications(bool trackChanges)
        {
            var jobApplications = await repositoryManager.JobApplicationRepository
                .GetAllJobApplications(trackChanges)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Department)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Job)
                .Include(j => j.User)
                .ToListAsync();

            return jobApplications.Select(j => new JobApplicationDto
            {
                JobApplicationId = j.JobApplicationId,
                JobListId = j.JobListId,
                UserId = j.UserId,
                AppDate = j.AppDate,
                ResumePath = j.ResumePath,
                Status = j.Status,
                Title = j.JobList?.Title,
                DepartmentName = j.JobList?.Department?.DepartmentName,
                JobName = j.JobList?.Job?.JobName,
                UserFullName = (j.User?.Name ?? "") + " " + (j.User?.LastName ?? "")
            });
        }

        public async Task<JobApplicationDto> GetJobApplicationById(int id, bool trackChanges)
        {
            var j = await repositoryManager.JobApplicationRepository
                .GetJobApplicationById(id, trackChanges)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Department)
                .Include(j => j.JobList)
                    .ThenInclude(jl => jl.Job)
                .FirstOrDefaultAsync();

            if (j == null)
                throw new EntityNotFoundException<JobApplication>(id);

            return new JobApplicationDto
            {
                JobApplicationId = j.JobApplicationId,
                JobListId = j.JobListId,
                UserId = j.UserId,
                AppDate = j.AppDate,
                ResumePath = j.ResumePath,
                Status = j.Status,
                Title = j.JobList?.Title,
                DepartmentName = j.JobList?.Department?.DepartmentName,
                JobName = j.JobList?.Job?.JobName
            };
        }

       public async Task<IEnumerable<JobApplicationDto>> GetApplicationsByCandidateId(int candidateId)
{
    var applications = await repositoryManager.JobApplicationRepository
        .FindByCondition(a => a.UserId == candidateId, false)
        .Include(a => a.JobList)
            .ThenInclude(jl => jl.Department)
        .Include(a => a.JobList)
            .ThenInclude(jl => jl.Job)
        .ToListAsync();

    return applications.Select(app => new JobApplicationDto
    {
        JobApplicationId = app.JobApplicationId,
        JobListId = app.JobListId,
        UserId = app.UserId,
        AppDate = app.AppDate,
        ResumePath = app.ResumePath,
        Status = app.Status,
        Title = app.JobList?.Title,
        DepartmentName = app.JobList?.Department?.DepartmentName,
        JobName = app.JobList?.Job?.JobName,
        JobList = mapper.Map<JobListDto>(app.JobList)
    });
}



        public async Task UpdateJobApplication(int id, JobApplicationUpdateDto jobApplication, bool trackChanges)
{
    var jobApplicationEntity = await repositoryManager.JobApplicationRepository
        .GetJobApplicationById(id, trackChanges)
        .FirstOrDefaultAsync();

    if (jobApplicationEntity == null)
        throw new EntityNotFoundException<JobApplication>(id);

    // Sadece durumu güncelle
    jobApplicationEntity.Status = jobApplication.Status;

    await repositoryManager.Save();
}

    }
}
