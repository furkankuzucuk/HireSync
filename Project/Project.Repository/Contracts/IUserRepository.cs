using Project.Entities;

namespace Project.Repository.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    IQueryable<User> GetAllUsers(bool trackChanges);
    IQueryable<User> GetUserById(int id , bool trakChanges);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

}