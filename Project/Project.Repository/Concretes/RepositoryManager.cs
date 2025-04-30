
using Microsoft.EntityFrameworkCore.Storage;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ILoginRepository> _loginRepository;
    private readonly Lazy<IJobRepository> _jobRepository;
    private readonly Lazy<IDepartmentRepository> _departmentRepository;
    private readonly Lazy<IJobApplicationRepository> _jobApplicationRepository;
    private readonly Lazy<IJobListRepository> _jobListRepository;
    private readonly Lazy<ILeaveRequestRepository> _leaveRepository;
    private readonly Lazy<IPerformanceReviewRepository> _performanceRepository;
    private readonly Lazy<ISatisfactionSurveyRepository> _satisfactionSurveyRepository;
    private readonly Lazy<ICandidateRepository> _candidateRepository;
    private readonly Lazy<IExamRepository> _examRepository;
    private readonly Lazy<IUserExamRepository> _userExamRepository;
    private readonly Lazy<IQuestonRepository> _questionRepository;

    public RepositoryManager(RepositoryContext repositoryContext){
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_repositoryContext));
        _loginRepository = new Lazy<ILoginRepository>(() => new LoginRepository(_repositoryContext));
        _jobRepository = new Lazy<IJobRepository>(() => new JobRepository(_repositoryContext));
        _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
        _jobApplicationRepository = new Lazy<IJobApplicationRepository>(() => new JobApplicationRepository(_repositoryContext));
        _jobListRepository = new Lazy<IJobListRepository>(() => new JobListRepository(_repositoryContext));
        _leaveRepository = new Lazy<ILeaveRequestRepository>(() => new LeaveRequestRepository(_repositoryContext));
        _performanceRepository = new Lazy<IPerformanceReviewRepository>(() => new PerformanceReviewRepository(_repositoryContext));
        _satisfactionSurveyRepository = new Lazy<ISatisfactionSurveyRepository>(() => new SatisfactionSurveyRepository(_repositoryContext));
        _examRepository = new Lazy<IExamRepository>(() => new ExamRepository(_repositoryContext));
        _candidateRepository = new Lazy<ICandidateRepository>(() => new CandidateRepository(_repositoryContext));
        _userExamRepository = new Lazy<IUserExamRepository>(() => new UserExamRepository(_repositoryContext));
        _questionRepository = new Lazy<IQuestonRepository>(() => new QuestionRepository(_repositoryContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public ILoginRepository LoginRepository => _loginRepository.Value;
    public IJobRepository JobRepository => _jobRepository.Value;
    public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;
    public IJobApplicationRepository JobApplicationRepository => _jobApplicationRepository.Value;
    public IJobListRepository JobListRepository => _jobListRepository.Value;
    public ILeaveRequestRepository LeaveRequestRepository => _leaveRepository.Value;
    public IPerformanceReviewRepository PerformanceReviewRepository => _performanceRepository.Value;
    public ISatisfactionSurveyRepository SatisfactionSurveyRepository => _satisfactionSurveyRepository.Value;
    public IExamRepository ExamRepository => _examRepository.Value;
    public ICandidateRepository CandidateRepository => _candidateRepository.Value;
    public IUserExamRepository UserExamRepository => _userExamRepository.Value;
    public IQuestonRepository QuestonRepository => _questionRepository.Value;

    public async Task Save()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}