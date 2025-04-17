using System.Linq.Expressions;
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateUser(User user)
    {
        Create(user);
    }

    public void DeleteUser(User user)
    {
        Delete(user);
    }

    public IQueryable<User> GetAllUsers(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public IQueryable<User> GetUserById(int id, bool trackChanges)
    {
        return FindByCondition(m=> m.UserId == id, trackChanges);
    }

    public void UpdateUser(User user)
    {
        Update(user);
    }
}