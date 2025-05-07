using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Login;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Project.Services.Concretes;

public class LoginService : ILoginService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;
    private readonly IConfiguration _configuration;
    private IEmailService _emailService;

    public LoginService(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
        _configuration = configuration;
    }

    public async Task<LoginDto> CreateLogin(LoginDtoInsertion loginDto)
    {
        var user = await repositoryManager.UserRepository
            .FindByCondition(u => u.Email == loginDto.Mail, trackChanges: false)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new EntityNotFoundException<User>(loginDto.Mail);
        }

        loginDto.Password = HashPassword(loginDto.Password);
        var loginEntity = mapper.Map<Login>(loginDto);
        loginEntity.UserId = user.UserId;

        repositoryManager.LoginRepository.CreateLogin(loginEntity);
        await repositoryManager.Save();

        return mapper.Map<LoginDto>(loginEntity);
    }

    public async Task<LoginDto> GetLoginById(int id, bool trackChanges)
    {
        var loginEntity = await repositoryManager.LoginRepository.GetLoginById(id, trackChanges).FirstOrDefaultAsync();
        if (loginEntity == null)
        {
            throw new UserNotFoundException(id);
        }
        return mapper.Map<LoginDto>(loginEntity);
    }

    public async Task UpdateLogin(int id, LoginDtoUpdate loginDto, bool trackChanges)
    {
        var loginEntity = await repositoryManager.LoginRepository.GetLoginById(id, trackChanges).FirstOrDefaultAsync();
        if (loginEntity == null)
        {
            throw new UserNotFoundException(id);
        }

        // Eğer password güncelleniyorsa, hashle!
        if (!string.IsNullOrEmpty(loginDto.Password))
        {
            loginDto.Password = HashPassword(loginDto.Password);
        }

        mapper.Map(loginDto, loginEntity);
        await repositoryManager.Save();
    }

    public async Task<LoginResponseDto> AuthenticateUser(LoginAuthenticationDto loginDto)
    {
        // 🔥 Giriş şifresini hash'liyoruz
        var hashedPassword = HashPassword(loginDto.Password);

        var loginJW = await repositoryManager.LoginRepository
            .FindByCondition(u => u.UserName == loginDto.UserName && u.Password == hashedPassword, false)
            .Include(u => u.User)
            .FirstOrDefaultAsync();

        if (loginJW == null || loginJW.User == null)
            return null;

        var role = loginJW.User.RoleName;
        var token = GenerateJwtToken(loginJW);

        return new LoginResponseDto
        {
            Token = token,
            Role = role
        };
    }

    private string GenerateJwtToken(Login user)
    {
    var role = user.User.RoleName ?? "Candidate"; 
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, role),
        new Claim("UserId", user.UserId.ToString()), 
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


    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }

    public async Task<bool> GeneratePasswordResetToken(string email)
    {
        var login = await repositoryManager.LoginRepository.GetLoginByEmailAsync(email, false);
        if (login == null) return false;

        // Token oluştur (örneğin GUID)
        var token = Guid.NewGuid().ToString();
        login.PasswordResetToken = token;
        login.ResetTokenExpires = DateTime.UtcNow.AddHours(1); // 1 saat geçerli
        
        repositoryManager.LoginRepository.UpdateLogin(login);
        await repositoryManager.Save();

        // Email gönder
        var resetLink = $"https://yourapp.com/reset-password?token={token}";
        await _emailService.SendPasswordResetEmailAsync(email, resetLink);

        return true;
    }

    public async Task<bool> ResetPassword(string token, string newPassword)
    {
        var login = await repositoryManager.LoginRepository.GetLoginByResetTokenAsync(token, false);
        if (login == null || login.ResetTokenExpires < DateTime.UtcNow)
        {
            return false;
        }

        // Şifreyi güncelle
        login.Password = HashPassword(newPassword); // Şifreleme yönteminize göre güncelleyin
        login.PasswordResetToken = null;
        login.ResetTokenExpires = null;
        
        repositoryManager.LoginRepository.UpdateLogin(login);
        await repositoryManager.Save();

        return true;
    }
}