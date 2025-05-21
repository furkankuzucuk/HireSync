using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.User;
using Project.Services.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Presentation.Controller
{
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
        [Authorize(Roles = "Admin")] // Sadece admin tüm kullanıcıları görebilir
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await serviceManager.UserService.GetAllUsers(false);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize] // Yetkilendirme gerekli
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await serviceManager.UserService.GetUserById(id, false);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("create-user")]
        [Authorize(Roles = "Admin")] // Sadece admin yeni kullanıcı oluşturabilir
        public async Task<IActionResult> CreateUser([FromBody] UserDtoInsertion user)
        {
            if (user == null)
                return BadRequest("User data is null");

            var createdUser = await serviceManager.UserService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPost("create-candidate")]
        [AllowAnonymous] // Aday kayıt işlemi açık olabilir
        public async Task<IActionResult> CreateCandidateUser([FromBody] UserDtoCandidateInsert user)
        {
            if (user == null)
                return BadRequest("User data is null");

            // RoleName "Candidate" olarak atanır, kontrol gerekmez
            var createdUser = await serviceManager.UserService.CreateCandidateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        [Authorize] // Kullanıcı veya admin güncelleme yapabilir, isteğe göre rol kontrolü eklenebilir
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDtoUpdate user)
        {
            if (user == null)
                return BadRequest("User data is null");

            await serviceManager.UserService.UpdateUser(id, user, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Sadece admin kullanıcı silebilir
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await serviceManager.UserService.DeleteOneUser(id, false);
            return NoContent();
        }

        // -------------------------
        // Yeni eklenen: Profil ve Onay Mesajı
        [HttpGet("profile")]
[Authorize]
public async Task<IActionResult> GetUserProfile()
{
    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
    if (userIdClaim == null)
        return Unauthorized();

    int userId = int.Parse(userIdClaim.Value);

    var user = await serviceManager.UserService.GetUserById(userId, false);

    var approvedApplications = await serviceManager.JobApplicationService.GetApplicationsByCandidateId(userId);

    var approvedApplication = approvedApplications.FirstOrDefault(a =>
        a.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ||
        a.Status.Equals("Onaylandı", StringComparison.OrdinalIgnoreCase));

    string message = null;
    if (approvedApplication != null)
    {
        var jobTitle = approvedApplication.Title ?? "ilgili pozisyon";
        message = $"Tebrikler, {jobTitle} pozisyonundaki başvurunuz onaylanmıştır. Kolay gelsin.";
    }

    return Ok(new
    {
        User = user,
        OnayMesaji = message
    });
}


    }
}
