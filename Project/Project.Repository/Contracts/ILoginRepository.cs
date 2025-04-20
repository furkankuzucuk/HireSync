
using Project.Entities;

namespace Project.Repository.Contracts;

public interface ILoginRepository : IRepositoryBase<Login>
{
    IQueryable<Login> GetLoginById(int id , bool trackChanges);
    void CreateLogin(Login login);
    void UpdateLogin(Login login);
}