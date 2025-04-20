
namespace Project.Services.Contracts
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(bool trackChanges);
        Task<T> GetById(int id, bool trackChanges);
        Task Create(T entity);
        Task Update(int id, T entity, bool trackChanges);
        Task Delete(int id, bool trackChanges);
    }
}