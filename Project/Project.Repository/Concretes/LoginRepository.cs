
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

    public IQueryable<Login> GetLoginById(int id, bool trackChanges)
    {
         return FindByCondition(m=> m.LoginId == id, trackChanges);
    }

    public void UpdateLogin(Login login)
    {
        Update(login);
    }
}