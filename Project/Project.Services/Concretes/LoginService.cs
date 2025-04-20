
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Login;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes;

public class LoginService : ILoginService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public LoginService(IRepositoryManager repositoryManager,IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }
    public async Task<LoginDto> CreateLogin(LoginDtoInsertion loginDto)
    {
        
            var loginEntity = mapper.Map<Login>(loginDto);
            repositoryManager.LoginRepository.CreateLogin(loginEntity);
            await repositoryManager.Save();
            return mapper.Map<LoginDto>(loginEntity);
    }

    public async Task<LoginDto> GetLoginById(int id, bool trackChanges)
    {
        var loginEntity = await repositoryManager.LoginRepository.GetLoginById(id,trackChanges).FirstOrDefaultAsync();
        if(loginEntity == null){
            throw new UserNotFoundException(id);
        }
        return mapper.Map<LoginDto>(loginEntity);
    }

    public async Task UpdateLogin(int id,LoginDtoUpdate loginDto,bool trackChanges)
    {
        var loginEntity = await repositoryManager.LoginRepository.GetLoginById(id,trackChanges).FirstOrDefaultAsync();
        if(loginEntity == null){
            throw new UserNotFoundException(id);
        }
        mapper.Map(loginDto,loginEntity);
        await repositoryManager.Save();

    }
    public async Task<LoginResponseDto> AuthenticateUser(LoginDto loginDto)
        {
            var user = await repositoryManager.LoginRepository
                .FindByCondition(u => u.UserName == loginDto.UserName && u.Password == loginDto.Password, false)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var role = user.Role; // Role bazlı yönlendirme için

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Role = role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role), // Role bilgisi ekleniyor
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}