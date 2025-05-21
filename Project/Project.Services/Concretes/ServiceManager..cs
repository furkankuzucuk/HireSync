
using AutoMapper;
using Microsoft.Extensions.Options;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager repositoryManager;
    private readonly Lazy<IUserService> userService;
    private readonly Lazy<ILoginService> loginService;
    private readonly Lazy<IJobService> jobService;
    private readonly Lazy<IDepartmentService> departmentService;
    private readonly Lazy<IJobApplicationService> jobApplicationService;
    private readonly Lazy<IJobListService> jobListService;
    private readonly Lazy<ILeaveRequestService> leaveRequestService;
    private readonly Lazy<IPerformanceReviewService> performanceReviewService;
    private readonly Lazy<ISatisfactionSurveyService> satisfactionSurveyService;
    private readonly Lazy<ISurveyAnswerService> surveyAnswerService;
    private readonly Lazy<ISurveyQuestionService> surveyQuestionService;
    private readonly Lazy<IExamService> examService;
    private readonly Lazy<IUserExamService> userExamService;
    private readonly Lazy<IQuestionService> questionService;
    private readonly Lazy<IEmailService> emailService;
    private readonly Lazy<IAnnouncementService> announcementService;
    private readonly IMapper mapper;


    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
        userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        emailService = new Lazy<IEmailService>(() => new EmailService(smtpSettings));
        loginService = new Lazy<ILoginService>(() => new LoginService(repositoryManager, mapper, configuration, emailService.Value));
        jobService = new Lazy<IJobService>(() => new JobService(repositoryManager, mapper));
        departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, mapper));
        jobApplicationService = new Lazy<IJobApplicationService>(() => new JobApplicationService(repositoryManager, mapper));
        jobListService = new Lazy<IJobListService>(() => new JobListService(repositoryManager, mapper));
        leaveRequestService = new Lazy<ILeaveRequestService>(() => new LeaveRequestService(repositoryManager, mapper));
        performanceReviewService = new Lazy<IPerformanceReviewService>(() => new PerformanceReviewService(repositoryManager, mapper));
        satisfactionSurveyService = new Lazy<ISatisfactionSurveyService>(() => new SatisfactionSurveyService(repositoryManager, mapper));
        surveyAnswerService = new Lazy<ISurveyAnswerService>(() => new SurveyAnswerService(repositoryManager, mapper));
        surveyQuestionService = new Lazy<ISurveyQuestionService>(() => new SurveyQuestionService(repositoryManager, mapper));
        examService = new Lazy<IExamService>(() => new ExamService(repositoryManager, mapper));
        userExamService = new Lazy<IUserExamService>(() => new UserExamService(repositoryManager, mapper));
        questionService = new Lazy<IQuestionService>(() => new QuestionService(repositoryManager, mapper));
        announcementService = new Lazy<IAnnouncementService>(() => new AnnouncementService(repositoryManager, mapper));
    }

    public IUserService UserService => userService.Value;
    public ILoginService LoginService => loginService.Value;
    public IJobService JobService => jobService.Value;
    public IDepartmentService DepartmentService => departmentService.Value;
    public IJobApplicationService JobApplicationService => jobApplicationService.Value;
    public IJobListService JobListService => jobListService.Value;
    public ILeaveRequestService LeaveRequestService => leaveRequestService.Value;
    public IPerformanceReviewService PerformanceReviewService => performanceReviewService.Value;
    public ISatisfactionSurveyService SatisfactionSurveyService => satisfactionSurveyService.Value;
    public ISurveyAnswerService SurveyAnswerService => surveyAnswerService.Value;
    public ISurveyQuestionService SurveyQuestionService => surveyQuestionService.Value;
    public IExamService ExamService => examService.Value;
    public IUserExamService UserExamService => userExamService.Value;
    public IQuestionService QuestionService => questionService.Value;
    public IEmailService EmailService => emailService.Value;

    public IAnnouncementService AnnouncementService => announcementService.Value;

    public async Task SaveAsync() => await repositoryManager.Save();
}