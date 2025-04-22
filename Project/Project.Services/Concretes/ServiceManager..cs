
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
    private readonly IMapper mapper;

    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper,IConfiguration configuration)
    {
        this.repositoryManager = repositoryManager;
        userService = new Lazy<IUserService>(() => new UserService(repositoryManager,mapper));
        loginService = new Lazy<ILoginService>(() => new LoginService(repositoryManager,mapper,configuration));
        jobService = new Lazy<IJobService>(() => new JobService(repositoryManager,mapper));
        departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager,mapper));
    }

    public IUserService UserService => userService.Value;
    public ILoginService LoginService => loginService.Value;
    public IJobService JobService => jobService.Value;
    public IDepartmentService DepartmentService => departmentService.Value;
    public async Task SaveAsync() => await repositoryManager.Save();
}