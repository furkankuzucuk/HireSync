
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ILoginRepository> _loginRepository;
    private readonly Lazy<IJobRepository> _jobRepository;
    private readonly Lazy<IDepartmentRepository> _departmentRepository;

    public RepositoryManager(RepositoryContext repositoryContext){
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_repositoryContext));
        _loginRepository = new Lazy<ILoginRepository>(() => new LoginRepository(_repositoryContext));
        _jobRepository = new Lazy<IJobRepository>(() => new JobRepository(_repositoryContext));
        _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public ILoginRepository LoginRepository => _loginRepository.Value;
    public IJobRepository JobRepository => _jobRepository.Value;
    public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

    public async Task Save()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}