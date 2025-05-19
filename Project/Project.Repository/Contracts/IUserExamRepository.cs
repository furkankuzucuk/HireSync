
using Project.Entities;

namespace Project.Repository.Contracts;

public interface IUserExamRepository : IRepositoryBase<UserExam>
{
    IQueryable<UserExam> GetAllUsers(bool trackChanges);
    IQueryable<UserExam> GetUserById(int id , bool trakChanges);
    Task<IEnumerable<UserExam>> GetUserExamsByUserIdAsync(int userId, bool trackChanges);
    IQueryable<UserExam> GetUserExamByExamId(int examId, bool trakChanges);
    void CreateUser(UserExam userExam);
    void UpdateUser(UserExam userExam);
    void DeleteUser(UserExam user);
}