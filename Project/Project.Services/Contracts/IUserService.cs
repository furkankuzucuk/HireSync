
using Project.Entities;
using Project.Entities.DataTransferObjects.User;

namespace Project.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers(bool trackChanges);
    Task<UserDto> GetUserById(int id,bool trackChanges);
    Task<UserDto> CreateUser(UserDtoInsertion user);
    Task UpdateUser(int id,UserDtoUpdate user,bool trackChanges);
    Task DeleteOneUser(int id,bool trackChanges);
}