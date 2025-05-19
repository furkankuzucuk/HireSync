using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/userexams")]
    public class UserExamController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserExamController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserExams()
        {
            var userExams = await _serviceManager.UserExamService.GetAllUsers(false);
            return Ok(userExams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserExamById(int id)
        {
            var userExam = await _serviceManager.UserExamService.GetUserById(id, false);
            return Ok(userExam);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserExamsByUserId(int userId)
        {
            var exams = await _serviceManager.UserExamService.GetUserExamsByUserId(userId, false);
            return Ok(exams);
        }

        [HttpGet("user/{examId}")]
        public async Task<IActionResult> GetUserExamByExamId(int examId)
        {
            var exams = await _serviceManager.UserExamService.GetUserExamByExamId(examId, false);
            return Ok(exams);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredUserExams([FromQuery] int? userId, [FromQuery] int? examId)
        {
            var results = await _serviceManager.UserExamService.GetFilteredUserExams(userId, examId, false);
            return Ok(results);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserExams()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim.Value);

            var userExams = await _serviceManager.UserExamService.GetUserExamsByUserId(userId, false);
            return Ok(userExams);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserExam([FromBody] UserExamInsertDto userExamDto)
        {
            if (userExamDto == null)
                return BadRequest("User exam data is null.");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null){
                return Unauthorized("User ID not found in token.");
            }
            int userId = int.Parse(userIdClaim.Value);

            var createdUserExam = await _serviceManager.UserExamService.CreateUser(userId,userExamDto);
            return CreatedAtAction(nameof(GetUserExamById), new { id = createdUserExam.UserExamId }, createdUserExam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserExam(int id, [FromBody] UserExamUpdateDto userExamDto)
        {
            if (userExamDto == null)
                return BadRequest("User exam data is null.");

            await _serviceManager.UserExamService.UpdateUser(id, userExamDto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserExam(int id)
        {
            await _serviceManager.UserExamService.DeleteOneUser(id, false);
            return NoContent();
        }

        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> SubmitExamAnswers([FromBody] UserExamAnswerDto answerDto)
        {
            if (answerDto == null || answerDto.Answers == null || !answerDto.Answers.Any())
                return BadRequest("Invalid answer data.");

            // Soruları examId ile çekiyoruz
            var questions = await _serviceManager.QuestionService.GetQuestionsByExamId(answerDto.ExamId, false);
            if (questions == null || !questions.Any())
                return NotFound("Questions not found for the given exam.");

            int correctCount = 0;

            foreach (var question in questions)
            {
                if (answerDto.Answers.TryGetValue(question.QuestionId, out string userAnswer))
                {
                    if (string.Equals(userAnswer.Trim(), question.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                    }
                }
            }

            int score = (int)((double)correctCount / questions.Count() * 100);

            // Kullanıcı bilgisi token’dan alınır
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim.Value);

            // Veritabanına sonucu kaydet
            var userExamDto = new UserExamInsertDto
            {
                ExamId = answerDto.ExamId,
                //UserId = userId,
                Score = score
            };

            var createdUserExam = await _serviceManager.UserExamService.CreateUser(userId, userExamDto);

            return Ok(new
            {
                Score = score,
                CorrectAnswers = correctCount,
                TotalQuestions = questions.Count(),
                UserExamId = createdUserExam.UserExamId
            });
        }


    }
}
