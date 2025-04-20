
namespace Project.Repository.Contracts;

public interface IRepositoryManager
{

    IUserRepository UserRepository { get; }
    ILoginRepository LoginRepository {get; }
    Task Save();
}