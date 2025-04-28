
using Project.Entities;

namespace Project.Repository.Contracts;

public interface IUserExamRepository : IRepositoryBase<UserExam>
{
    IQueryable<UserExam> GetAllUsers(bool trackChanges);
    IQueryable<UserExam> GetUserById(int id , bool trakChanges);
    void CreateUser(UserExam userExam);
    void UpdateUser(UserExam userExam);
    void DeleteUser(UserExam user);
}