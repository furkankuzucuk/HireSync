
using AutoMapper;
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
    private readonly Lazy<IExamService> examService;
    private readonly Lazy<ICandidateService> candidateService;
    private readonly Lazy<IUserExamService> userExamService;
    private readonly Lazy<IQuestionService> questionService;
    private readonly Lazy<IEmailService> emailService;
    private readonly IMapper mapper;
    private SmtpSettings smtpSettings1;

    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,IConfiguration configuration)
    {
        this.repositoryManager = repositoryManager;
        userService = new Lazy<IUserService>(() => new UserService(repositoryManager,mapper));
        loginService = new Lazy<ILoginService>(() => new LoginService(repositoryManager,mapper,configuration));
        jobService = new Lazy<IJobService>(() => new JobService(repositoryManager,mapper));
        departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager,mapper));
        jobApplicationService = new Lazy<IJobApplicationService> (() => new JobApplicationService(repositoryManager,mapper));
        jobListService = new Lazy<IJobListService> (() => new JobListService(repositoryManager,mapper));
        leaveRequestService = new Lazy<ILeaveRequestService> (() => new LeaveRequestService(repositoryManager,mapper));
        performanceReviewService = new Lazy<IPerformanceReviewService> (() => new PerformanceReviewService(repositoryManager,mapper));
        satisfactionSurveyService = new Lazy<ISatisfactionSurveyService>(() => new SatisfactionSurveyService(repositoryManager,mapper));
        examService = new Lazy<IExamService>(() => new ExamService(repositoryManager,mapper));
        candidateService = new Lazy<ICandidateService>(() => new CandidateService(repositoryManager,mapper));
        userExamService = new Lazy<IUserExamService>(() => new UserExamService(repositoryManager,mapper));
        questionService = new Lazy<IQuestionService>(() => new QuestionService(repositoryManager,mapper));
        emailService = new Lazy<IEmailService>(() => new EmailService(smtpSettings1));
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
    public IExamService ExamService => examService.Value;
    public ICandidateService CandidateService => candidateService.Value;
    public IUserExamService UserExamService => userExamService.Value;
    public IQuestionService QuestionService => questionService.Value;
    public async Task SaveAsync() => await repositoryManager.Save();
}