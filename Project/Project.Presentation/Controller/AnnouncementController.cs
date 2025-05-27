using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Announcement;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/announcements")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public AnnouncementController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await serviceManager.AnnouncementService.GetAllAnnouncements(false);
            return Ok(announcements);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementInsertDto dto)
        {
            if (dto is null)
                return BadRequest("Boş veri gönderilemez.");

            var created = await serviceManager.AnnouncementService.CreateAnnouncement(dto);
            return CreatedAtAction(nameof(GetAllAnnouncements), new { id = created.AnnouncementId }, created);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await serviceManager.AnnouncementService.DeleteAnnouncement(id, false);
            return NoContent();
        }
    }
}
