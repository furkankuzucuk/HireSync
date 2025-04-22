using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext context) : base(context) { }

        public IQueryable<Department> GetAllDepartments(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<Department> GetDepartmentById(int id, bool trackChanges) =>
            FindByCondition(d => d.DepartmentId == id, trackChanges);

        public void CreateDepartment(Department department) =>
            Create(department);

        public void UpdateDepartment(Department department) =>
            Update(department);

        public void DeleteDepartment(Department department) =>
            Delete(department);
    }
}
