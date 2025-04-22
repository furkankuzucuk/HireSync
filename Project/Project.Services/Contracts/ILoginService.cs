
using Project.Entities.DataTransferObjects.Login;

namespace Project.Services.Contracts;

public interface ILoginService
{
    Task<LoginDto> GetLoginById (int id , bool trackChanges);
    Task UpdateLogin (int id,LoginDtoUpdate loginDto,bool trackChanges);
    Task<LoginDto> CreateLogin (LoginDtoInsertion loginDto);
    Task<LoginResponseDto> AuthenticateUser(LoginAuthenticationDto loginDto);
}