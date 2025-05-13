using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/useranswers")]
    public class UserAnswerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserAnswerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("submit-exam")]
        public async Task<IActionResult> SubmitExam([FromBody] ExamSubmissionDto submission)
        {
            if (submission == null || submission.Answers == null)
                return BadRequest("Invalid submission data");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim.Value);
            
            var result = await _serviceManager.UserAnswerService.SubmitExamAnswers(
                userId, 
                submission.ExamId, 
                submission.Answers
            );

            return Ok(result);
        }

        [HttpGet("exam/{examId}/user/{userId}")]
        public async Task<IActionResult> GetUserAnswersForExam(int examId, int userId)
        {
            var answers = await _serviceManager.UserAnswerService.GetUserAnswersForExam(userId, examId);
            return Ok(answers);
        }
    }
}