using Project.Entities;
using Project.Entities.DataTransferObjects.Department;

namespace Project.Services.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartments(bool trackChanges);
        Task<DepartmentDto> GetDepartmentById(int id, bool trackChanges);
        Task<DepartmentDto> CreateDepartment(DepartmentInsertDto department);
        Task UpdateDepartment(int id, DepartmentUpdateDto department, bool trackChanges);
        Task DeleteDepartment(int id, bool trackChanges);
    }
}
