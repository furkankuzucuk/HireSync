using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        IQueryable<Department> GetAllDepartments(bool trackChanges);
        IQueryable<Department> GetDepartmentById(int id, bool trackChanges);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
    }
}
