
namespace Project.Services.Contracts;

public interface IServiceManager
{
    IUserService UserService {get; }
    ILoginService LoginService {get; }
    IJobService JobService {get; }
    IDepartmentService DepartmentService {get; }
}