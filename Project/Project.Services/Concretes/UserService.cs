using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.User;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class UserService : IUserService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper _mapper;
    public UserService(IRepositoryManager manager,IMapper mapper)
    {
        repositoryManager = manager;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateCandidateUser(UserDtoCandidateInsert user)
    {
        var userEntity = _mapper.Map<User>(user); //adayları ekleme fonksiyonu
        repositoryManager.UserRepository.CreateUser(userEntity);
        await repositoryManager.Save();
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> CreateUser(UserDtoInsertion user)
    {
        var userEntity = _mapper.Map<User>(user);
        repositoryManager.UserRepository.CreateUser(userEntity);
        await repositoryManager.Save();
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task DeleteOneUser(int id, bool trackChanges)
    {
        var userEntity = await repositoryManager.UserRepository.GetUserById(id,trackChanges).FirstOrDefaultAsync();
        if(userEntity == null){
            throw new UserNotFoundException(id);
        }
        repositoryManager.UserRepository.DeleteUser(userEntity);
        await repositoryManager.Save();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers(bool trackChanges)
    {
        var users = await repositoryManager.UserRepository.GetAllUsers(trackChanges).ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users); 
    }

    public async Task<UserDto> GetUserById(int id, bool trackChanges)
    {
        var userEntity = await repositoryManager.UserRepository.GetUserById(id, trackChanges).FirstOrDefaultAsync();
        if (userEntity == null)
        {
            throw new UserNotFoundException(id);
        }
        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task UpdateUser(int id, UserDtoUpdate user, bool trackChanges)
    {
        var userEntity = await repositoryManager.UserRepository.GetUserById(id, trackChanges).FirstOrDefaultAsync();
        if (userEntity == null)
        {
            throw new UserNotFoundException(id);
        }
        _mapper.Map(user, userEntity);
        await repositoryManager.Save();
    }
}