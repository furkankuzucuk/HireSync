
using Project.Entities;

namespace Project.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers(bool trackChanges);
    Task<User> GetUserById(int id,bool trackChanges);
    Task<User> CreateUser(User user);
    Task UpdateUser(int id,User user,bool trackChanges);
    Task DeleteOneUser(int id,bool trackChanges);
}