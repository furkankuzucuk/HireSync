
using Project.Repository.Concretes;
using Project.Services.Contracts;

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
    ISurveyAnswerRepository SurveyAnswerRepository {get; }
    ISurveyQuestionRepository SurveyQuestionRepository {get; }
    IExamRepository ExamRepository {get; }
    ICandidateRepository CandidateRepository {get; }
    IUserExamRepository UserExamRepository {get; }
    IQuestionRepository QuestionRepository {get; }
    Task Save();
}