using Project.Entities;

namespace Project.Repository.Contracts;

public interface ILoginRepository : IRepositoryBase<Login>
{
    IQueryable<Login> GetLoginById(int id , bool trackChanges);
    

    void CreateLogin(Login login);
    void UpdateLogin(Login login);
    Task<Login> GetLoginByEmailAsync(string email, bool trackChanges);
    Task<Login> GetLoginByResetTokenAsync(string token, bool trackChanges);
    Task<Login> GetLoginByUsernameAndPassword(string username, string password); // ✅ Eklenen metod
}
