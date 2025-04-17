using Microsoft.AspNetCore.Mvc;
using Project.Entities;
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
            return user != null ? Ok(user) : NotFound("User not found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await serviceManager.UserService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            await serviceManager.UserService.UpdateUser(id, user, false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await serviceManager.UserService.DeleteOneUser(id, false);
            return NoContent();
        }
    }