
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ILoginRepository> _loginRepository;

    public RepositoryManager(RepositoryContext repositoryContext){
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_repositoryContext));
        _loginRepository = new Lazy<ILoginRepository>(() => new LoginRepository(_repositoryContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public ILoginRepository LoginRepository => _loginRepository.Value;

    public async Task Save()
    {
        await _repositoryContext.SaveChangesAsync();
    }
}