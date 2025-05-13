using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.SurveyAnswer;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/surveyanswers")]
    public class SurveyAnswerController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public SurveyAnswerController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET api/surveyanswers
        [HttpGet]
        public async Task<IActionResult> GetAllSurveyAnswers()
        {
            var surveyAnswers = await serviceManager.SurveyAnswerService.GetAllSurveyAnswers(false);
            return Ok(surveyAnswers);
        }

        // GET api/surveyanswers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSurveyAnswerById(int id)
        {
            var surveyAnswer = await serviceManager.SurveyAnswerService.GetSurveyAnswerById(id, false);
            return Ok(surveyAnswer);
        }

        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> SubmitSurveyAnswers([FromBody] SurveySubmissionDto submission)
        {
            if (submission == null || submission.Answers == null || !submission.Answers.Any())
                return BadRequest("Invalid submission data");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            await serviceManager.SurveyAnswerService.SubmitSurveyAnswers(userId, submission);
            return Ok("Survey submitted successfully");
        }


        // POST api/surveyanswers
        [HttpPost]
        public async Task<IActionResult> CreateSurveyAnswer([FromBody] SurveyAnswerInsertDto surveyAnswerDto)
        {
            if (surveyAnswerDto == null)
                return BadRequest("Survey answer data is null");

            var createdSurveyAnswer = await serviceManager.SurveyAnswerService.CreateSurveyAnswer(surveyAnswerDto);
            return CreatedAtAction(nameof(GetSurveyAnswerById), new { id = createdSurveyAnswer.SurveyAnswerId }, createdSurveyAnswer);
        }

        // PUT api/surveyanswers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurveyAnswer(int id, [FromBody] SurveyAnswerUpdateDto surveyAnswerDto)
        {
            if (surveyAnswerDto == null)
                return BadRequest("Survey answer data is null");

            await serviceManager.SurveyAnswerService.UpdateSurveyAnswer(id, surveyAnswerDto, true);
            return NoContent();
        }

        // DELETE api/surveyanswers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyAnswer(int id)
        {
            await serviceManager.SurveyAnswerService.DeleteSurveyAnswer(id, false);
            return NoContent();
        }
    }
}
