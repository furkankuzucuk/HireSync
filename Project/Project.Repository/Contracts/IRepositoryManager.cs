
namespace Project.Repository.Contracts;

public interface IRepositoryManager
{

    IUserRepository UserRepository { get; }
    ILoginRepository LoginRepository {get; }
    IJobRepository JobRepository {get; }
    IDepartmentRepository DepartmentRepository {get; }
    Task Save();
}