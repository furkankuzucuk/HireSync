using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Department;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public DepartmentController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await serviceManager.DepartmentService.GetAllDepartments(false);
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await serviceManager.DepartmentService.GetDepartmentById(id, false);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentInsertDto dto)
        {
            if (dto is null)
                return BadRequest("Department data is null");

            var created = await serviceManager.DepartmentService.CreateDepartment(dto);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = created.DepartmentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto dto)
        {
            if (dto is null)
                return BadRequest("Department data is null");

            await serviceManager.DepartmentService.UpdateDepartment(id, dto, false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await serviceManager.DepartmentService.DeleteDepartment(id, false);
            return NoContent();
        }
    }
}
