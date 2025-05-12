
using Project.Entities.DataTransferObjects;

namespace Project.Services.Contracts;

public interface IUserExamService
{
    Task<IEnumerable<UserExamDto>> GetAllUsers(bool trackChanges);
    Task<UserExamDto> GetUserById(int id,bool trackChanges);
    Task<IEnumerable<UserExamDto>> GetUserExamsByUserId(int userId, bool trackChanges);
    Task<UserExamDto> CreateUser(int userId,UserExamInsertDto user);
    Task UpdateUser(int id,UserExamUpdateDto user,bool trackChanges);
    Task DeleteOneUser(int id,bool trackChanges);

}