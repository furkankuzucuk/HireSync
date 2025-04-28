
namespace Project.Services.Contracts;

public interface IServiceManager
{
    IUserService UserService {get; }
    ILoginService LoginService {get; }
    IJobService JobService {get; }
    IDepartmentService DepartmentService {get; }
    IJobApplicationService JobApplicationService {get; }
    IJobListService JobListService {get; }
    ILeaveRequestService LeaveRequestService {get; }
    IPerformanceReviewService PerformanceReviewService {get; }
    ISatisfactionSurveyService SatisfactionSurveyService {get; }
    ICandidateService CandidateService {get; }
    IExamService ExamService {get; }
    IUserExamService UserExamService {get;}
}