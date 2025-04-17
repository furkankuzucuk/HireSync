
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class ServiceManager : IServiceManager
{
    private readonly IRepositoryManager repositoryManager;
    private readonly Lazy<IUserService> userService;
    

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        this.repositoryManager = repositoryManager;
        userService = new Lazy<IUserService>(() => new UserService(repositoryManager));
    }

    public IUserService UserService => userService.Value;
    public async Task SaveAsync() => await repositoryManager.Save();
}