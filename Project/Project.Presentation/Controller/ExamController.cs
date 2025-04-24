using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Exam;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/exams")]
    public class ExamController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ExamController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await serviceManager.ExamService.GetAllExams(false);
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(int id)
        {
            var exam = await serviceManager.ExamService.GetExamById(id, false);
            return Ok(exam);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] ExamInsertDto examDto)
        {
            if (examDto == null)
                return BadRequest("Invalid exam data.");

            var createdExam = await serviceManager.ExamService.CreateExam(examDto);
            return CreatedAtAction(nameof(GetExamById), new { id = createdExam.ExamId }, createdExam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] ExamUpdateDto examDto)
        {
            if (examDto == null)
                return BadRequest("Invalid exam data.");

            await serviceManager.ExamService.UpdateExam(id, examDto, false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await serviceManager.ExamService.DeleteExam(id, false);
            return NoContent();
        }
    }
}
