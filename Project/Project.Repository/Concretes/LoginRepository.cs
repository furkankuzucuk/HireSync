
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class LoginRepository : RepositoryBase<Login>, ILoginRepository
{
    public LoginRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateLogin(Login login)
    {
        Create(login);
    }

    public async Task<Login> GetLoginByEmailAsync(string email, bool trackChanges)
    {
        return await FindByCondition(l => l.Mail.Equals(email), trackChanges)
                .Include(l => l.User) // User bilgisini de include ediyoruz
                .SingleOrDefaultAsync();
    }

    public IQueryable<Login> GetLoginById(int id, bool trackChanges)
    {
         return FindByCondition(m=> m.LoginId == id, trackChanges);
    }

    public async Task<Login> GetLoginByResetTokenAsync(string token, bool trackChanges)
    {
        return await FindByCondition(l => 
                    l.PasswordResetToken != null && 
                    l.PasswordResetToken.Equals(token) && 
                    l.ResetTokenExpires > DateTime.UtcNow, 
                trackChanges)
                .SingleOrDefaultAsync();
    }

    public void UpdateLogin(Login login)
    {
        Update(login);
    }
}