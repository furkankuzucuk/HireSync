
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
    ISurveyAnswerService SurveyAnswerService {get; }
    ISurveyQuestionService SurveyQuestionService {get; }
    ICandidateService CandidateService {get; }
    IExamService ExamService {get; }
    IQuestionService QuestionService {get; }
    IUserExamService UserExamService {get;}
    IEmailService EmailService {get;}
}