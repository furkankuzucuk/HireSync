using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/surveyquestions")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public SurveyQuestionController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET api/surveyquestions
        [HttpGet]
        public async Task<IActionResult> GetAllSurveyQuestions()
        {
            var surveyQuestions = await serviceManager.SurveyQuestionService.GetAllSurveyQuestions(false);
            return Ok(surveyQuestions);
        }

        // GET api/surveyquestions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyQuestionById(int id)
        {
            var surveyQuestion = await serviceManager.SurveyQuestionService.GetSurveyQuestionById(id, false);
            return Ok(surveyQuestion);
        }

        [HttpGet("survey/{surveyId}")]
        public async Task<IActionResult> GetSurveyQuestionsBySurveyId(int surveyId)
        {
            var questions = await serviceManager.SurveyQuestionService.GetSurveyQuestionsBySurveyId(surveyId, false);
            return Ok(questions);
        }

        [HttpGet("results/{surveyId}")]
        public async Task<IActionResult> GetSurveyResults(int surveyId)
        {
            var results = await serviceManager.SurveyQuestionService.GetSurveyResultsGroupedByQuestion(surveyId);
            return Ok(results);
        }

        // POST api/surveyquestions
        [HttpPost]
        public async Task<IActionResult> CreateSurveyQuestion([FromBody] SurveyQuestionInsertDto surveyQuestionDto)
        {
            if (surveyQuestionDto == null)
                return BadRequest("Survey question data is null");

            var createdSurveyQuestion = await serviceManager.SurveyQuestionService.CreateSurveyQuestion(surveyQuestionDto);
            return CreatedAtAction(nameof(GetSurveyQuestionById), new { id = createdSurveyQuestion.SurveyQuestionId }, createdSurveyQuestion);
        }

        // PUT api/surveyquestions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurveyQuestion(int id, [FromBody] SurveyQuestionUpdateDto surveyQuestionDto)
        {
            if (surveyQuestionDto == null)
                return BadRequest("Survey question data is null");

            await serviceManager.SurveyQuestionService.UpdateSurveyQuestion(id, surveyQuestionDto, true);
            return NoContent();
        }

        // DELETE api/surveyquestions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyQuestion(int id)
        {
            await serviceManager.SurveyQuestionService.DeleteSurveyQuestion(id, false);
            return NoContent();
        }
    }
}
