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
    }
}
