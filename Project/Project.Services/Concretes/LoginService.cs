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

namespace Project.Services.Concretes
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public LoginService(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration, IEmailService emailService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<LoginDto> CreateLogin(LoginDtoInsertion loginDto)
        {
            var user = await _repositoryManager.UserRepository
                .FindByCondition(u => u.Email == loginDto.Mail, trackChanges: false)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new EntityNotFoundException<User>(loginDto.Mail);

            loginDto.Password = HashPassword(loginDto.Password);
            var loginEntity = _mapper.Map<Login>(loginDto);
            loginEntity.UserId = user.UserId;

            _repositoryManager.LoginRepository.CreateLogin(loginEntity);
            await _repositoryManager.Save();

            return _mapper.Map<LoginDto>(loginEntity);
        }

        public async Task<LoginDto> GetLoginById(int id, bool trackChanges)
        {
            var loginEntity = await _repositoryManager.LoginRepository.GetLoginById(id, trackChanges).FirstOrDefaultAsync();
            if (loginEntity == null)
                throw new UserNotFoundException(id);

            return _mapper.Map<LoginDto>(loginEntity);
        }

        public async Task UpdateLogin(int id, LoginDtoUpdate loginDto, bool trackChanges)
        {
            var loginEntity = await _repositoryManager.LoginRepository.GetLoginById(id, trackChanges).FirstOrDefaultAsync();
            if (loginEntity == null)
                throw new UserNotFoundException(id);

            if (!string.IsNullOrEmpty(loginDto.Password))
                loginDto.Password = HashPassword(loginDto.Password);

            _mapper.Map(loginDto, loginEntity);
            await _repositoryManager.Save();
        }

        public async Task<LoginResponseDto> AuthenticateUser(LoginAuthenticationDto loginDto)
        {
            var login = await _repositoryManager.LoginRepository
                .GetLoginByUsernameAndPassword(loginDto.UserName, loginDto.Password);

            if (login == null)
                return null;

            var token = GenerateJwtToken(login);

            return new LoginResponseDto
            {
                Token = token,
                Role = login.User.RoleName,
                UserId = login.UserId
            };
        }

        private string GenerateJwtToken(Login login)
        {
            var role = login.User.RoleName ?? "Candidate";
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserId.ToString()),
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", login.UserId.ToString())
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
            var login = await _repositoryManager.LoginRepository.GetLoginByEmailAsync(email, false);
            if (login == null) return false;

            var token = Guid.NewGuid().ToString();
            login.PasswordResetToken = token;
            login.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

            _repositoryManager.LoginRepository.UpdateLogin(login);
            await _repositoryManager.Save();

            // Frontend reset-password URL'sini appsettings.json'dan al
            var frontendResetUrl = _configuration["Frontend:ResetPasswordUrl"];
            var resetLink = $"{frontendResetUrl}?token={token}";

            await _emailService.SendPasswordResetEmailAsync(email, resetLink);

            return true;
        }

        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            var login = await _repositoryManager.LoginRepository.GetLoginByResetTokenAsync(token, false);
            if (login == null || login.ResetTokenExpires < DateTime.UtcNow)
                return false;

            login.Password = HashPassword(newPassword);
            login.PasswordResetToken = null;
            login.ResetTokenExpires = null;

            _repositoryManager.LoginRepository.UpdateLogin(login);
            await _repositoryManager.Save();

            return true;
        }
    }
}
