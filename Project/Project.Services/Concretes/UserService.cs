using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class UserService : IUserService
{
    private readonly IRepositoryManager repositoryManager;
    
    public UserService(IRepositoryManager repositoryManager){
        this.repositoryManager = repositoryManager;
    }
    public async Task<User> CreateUser(User user)
    {
        repositoryManager.UserRepository.CreateUser(user);
         await repositoryManager.Save();
         return user;
    }

    public async Task DeleteOneUser(int id, bool trackChanges)
    {
        var userEntity = repositoryManager.UserRepository.GetUserById(id,trackChanges).FirstOrDefault();

        if (userEntity == null)
        {
        throw new UserNotFoundException(id); // Burada da özel hata fırlatılıyor
        }

        repositoryManager.UserRepository.DeleteUser(userEntity);
        await repositoryManager.Save();
    }

    public async Task<IEnumerable<User>> GetAllUsers(bool trackChanges)
    {
        var users = await repositoryManager.UserRepository.GetAllUsers(trackChanges).ToListAsync();
        return users;
    }

    public async Task<User> GetUserById(int id, bool trackChanges)
    {
        var userEntity = await repositoryManager.UserRepository.GetUserById(id,trackChanges).FirstOrDefaultAsync();
        if (userEntity == null)
    {
        throw new UserNotFoundException(id); // Burada da özel hata fırlatılıyor
    }
        return userEntity;
    }

    public async Task UpdateUser(int id, User user, bool trackChanges)
    {
        var userEntity = await repositoryManager.UserRepository.GetUserById(id,trackChanges).FirstOrDefaultAsync();

        if (userEntity == null)
    {
        throw new UserNotFoundException(id); // Burada da özel hata fırlatılıyor
    }
        repositoryManager.UserRepository.UpdateUser(userEntity);
        await repositoryManager.Save();

    }
}