using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.Department;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public DepartmentService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<DepartmentDto> CreateDepartment(DepartmentInsertDto department)
        {
            var entity = mapper.Map<Department>(department);
            repositoryManager.DepartmentRepository.CreateDepartment(entity);
            await repositoryManager.Save();
            return mapper.Map<DepartmentDto>(entity);
        }

        public async Task DeleteDepartment(int id, bool trackChanges)
        {
            var entity = await repositoryManager.DepartmentRepository.GetDepartmentById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Department>(id);

            repositoryManager.DepartmentRepository.DeleteDepartment(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments(bool trackChanges)
        {
            var departments = await repositoryManager.DepartmentRepository.GetAllDepartments(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> GetDepartmentById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.DepartmentRepository.GetDepartmentById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Department>(id);

            return mapper.Map<DepartmentDto>(entity);
        }

        public async Task UpdateDepartment(int id, DepartmentUpdateDto department, bool trackChanges)
        {
            var entity = await repositoryManager.DepartmentRepository.GetDepartmentById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<Department>(id);

            mapper.Map(department, entity);
            await repositoryManager.Save();
        }
    }
}
