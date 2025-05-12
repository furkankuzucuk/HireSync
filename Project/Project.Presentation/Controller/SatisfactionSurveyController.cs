using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.SatisfactionSurvey;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/satisfactionsurveys")]
    public class SatisfactionSurveyController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public SatisfactionSurveyController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSatisfactionSurveys()
        {
            var surveys = await serviceManager.SatisfactionSurveyService.GetAllSatisfactionSurveys(false);
            return Ok(surveys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSatisfactionSurveyById(int id)
        {
            var survey = await serviceManager.SatisfactionSurveyService.GetSatisfactionSurveyById(id, false);
            return Ok(survey);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetSurveysForLoggedInUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("UserId not found in token.");

            int userId = int.Parse(userIdClaim.Value);

            var surveys = await serviceManager.SatisfactionSurveyService.GetSurveysByUserDepartment(userId);
            return Ok(surveys);
        }       

        [HttpPost]
        public async Task<IActionResult> CreateSatisfactionSurvey([FromBody] SatisfactionSurveyInsertDto satisfactionSurveyDto)
        {
            if (satisfactionSurveyDto == null)
                return BadRequest("Satisfaction survey data is null");

            var createdSurvey = await serviceManager.SatisfactionSurveyService.CreateSatisfactionSurvey(satisfactionSurveyDto);
            return CreatedAtAction(nameof(GetSatisfactionSurveyById), new { id = createdSurvey.SatisfactionSurveyId }, createdSurvey);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSatisfactionSurvey(int id, [FromBody] SatisfactionSurveyUpdateDto satisfactionSurveyDto)
        {
            if (satisfactionSurveyDto == null)
                return BadRequest("Satisfaction survey data is null");

            await serviceManager.SatisfactionSurveyService.UpdateSatisfactionSurvey(id, satisfactionSurveyDto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSatisfactionSurvey(int id)
        {
            await serviceManager.SatisfactionSurveyService.DeleteSatisfactionSurvey(id, false);
            return NoContent();
        }
    }
}
