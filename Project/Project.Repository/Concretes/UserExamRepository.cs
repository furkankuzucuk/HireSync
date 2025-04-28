
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class UserExamRepository : RepositoryBase<UserExam>, IUserExamRepository
{
    public UserExamRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateUser(UserExam userExam)
    {
        Create(userExam);
    }

    public void DeleteUser(UserExam user)
    {
        Delete(user);
    }

    public IQueryable<UserExam> GetAllUsers(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public IQueryable<UserExam> GetUserById(int id, bool trackChanges)
    {
        return FindByCondition(m=> m.UserExamId == id, trackChanges);
    }

    public void UpdateUser(UserExam userExam)
    {
        Update(userExam);
    }
}