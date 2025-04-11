using Project.Entities;

namespace Project.Repository.Contracts;

public interface IUserRepository : IRepositoryBase<User>
{
    IQueryable<User> GetAll(bool trackChanges);
    IQueryable<User> GetById(int id , bool trakChanges);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

}