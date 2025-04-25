
namespace Project.Repository.Contracts;

public interface IRepositoryManager
{

    IUserRepository UserRepository { get; }
    ILoginRepository LoginRepository {get; }
    IJobRepository JobRepository {get; }
    IDepartmentRepository DepartmentRepository {get; }
    IJobApplicationRepository JobApplicationRepository {get; }
    IJobListRepository JobListRepository {get; }
    ILeaveRequestRepository LeaveRequestRepository {get; }
    IPerformanceReviewRepository PerformanceReviewRepository {get; }
    ISatisfactionSurveyRepository SatisfactionSurveyRepository {get; }
    IExamRepository ExamRepository {get; }
    ICandidateRepository CandidateRepository {get; }
    Task Save();
}