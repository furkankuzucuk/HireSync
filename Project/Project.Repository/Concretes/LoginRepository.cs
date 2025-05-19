using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Project.Repository.Concretes;

public class LoginRepository : RepositoryBase<Login>, ILoginRepository
{
    public LoginRepository(RepositoryContext context) : base(context) { }

    public void CreateLogin(Login login) => Create(login);

    public async Task<Login> GetLoginByEmailAsync(string email, bool trackChanges)
    {
        return await FindByCondition(l => l.Mail.Equals(email), trackChanges)
            .Include(l => l.User)
            .SingleOrDefaultAsync();
    }

    public IQueryable<Login> GetLoginById(int id, bool trackChanges) =>
        FindByCondition(l => l.LoginId == id, trackChanges);

    public async Task<Login> GetLoginByResetTokenAsync(string token, bool trackChanges)
    {
        return await FindByCondition(l =>
                    l.PasswordResetToken != null &&
                    l.PasswordResetToken.Equals(token) &&
                    l.ResetTokenExpires > DateTime.UtcNow,
                trackChanges)
            .SingleOrDefaultAsync();
    }

    public void UpdateLogin(Login login) => Update(login);

    public async Task<Login> GetLoginByUsernameAndPassword(string username, string password)
    {
        var hashedPassword = HashPassword(password);

        return await FindByCondition(
        l => l.UserName == username && l.Password == hashedPassword, false)
    .Include(l => l.User)
    .FirstOrDefaultAsync();

    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}
