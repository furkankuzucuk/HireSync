using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public QuestionController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions([FromQuery] bool trackChanges = false)
        {
            var questions = await serviceManager.QuestionService.GetAllQuestions(trackChanges);
            return Ok(questions);
        }

        // GET: api/questions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id, [FromQuery] bool trackChanges = false)
        {
            var question = await serviceManager.QuestionService.GetQuestionById(id, trackChanges);
            if (question == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }
            return Ok(question);
        }

        // POST: api/questions
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionInsertDto questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest("Question data is null.");
            }

            var createdQuestion = await serviceManager.QuestionService.CreateQuestion(questionDto);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.QuestionId }, createdQuestion);
        }

        // PUT: api/questions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionUpdateDto questionDto, [FromQuery] bool trackChanges = false)
        {
            if (questionDto == null)
            {
                return BadRequest("Question data is null.");
            }

            await serviceManager.QuestionService.UpdateQuestion(id, questionDto, trackChanges);
            return NoContent(); // Successfully updated
        }

        // DELETE: api/questions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, [FromQuery] bool trackChanges = false)
        {
            await serviceManager.QuestionService.DeleteQuestion(id, trackChanges);
            return NoContent(); // Successfully deleted
        }
    }
}
