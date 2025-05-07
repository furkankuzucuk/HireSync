using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Entities.DataTransferObjects.User;
using Project.Services.Concretes;
using Project.Services.Contracts;

namespace Project.Presentation.Controller;

[ApiController]
[Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await serviceManager.UserService.GetAllUsers(false);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await serviceManager.UserService.GetUserById(id, false);
            if(user == null){
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoInsertion user)
        {
            if(user is null){
                return BadRequest("User data is null");
            }
            var createdUser = await serviceManager.UserService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPost("create-candidate")] //Aday ekleme isteği
public async Task<IActionResult> CreateCandidateUser([FromBody] UserDtoCandidateInsert user)
{
    if(user is null)
    {
        return BadRequest("User data is null");
    }

    // RoleName zaten "Candidate" olarak atanacağı için burada ayrıca bir kontrol yapmamıza gerek yok
    var createdUser = await serviceManager.UserService.CreateCandidateUser(user);
    return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
}



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDtoUpdate user)
        {
            if(user is null){
                return BadRequest("User data is null");
            }
            await serviceManager.UserService.UpdateUser(id, user, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await serviceManager.UserService.DeleteOneUser(id, false);
            return NoContent();
        }
    }