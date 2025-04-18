
using AutoMapper;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager repositoryManager;
    private readonly Lazy<IUserService> userService;
    private readonly IMapper mapper;

    public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        userService = new Lazy<IUserService>(() => new UserService(repositoryManager,mapper));
    }

    public IUserService UserService => userService.Value;
    public async Task SaveAsync() => await repositoryManager.Save();
}