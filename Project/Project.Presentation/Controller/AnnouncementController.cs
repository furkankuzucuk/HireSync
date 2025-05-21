using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Anouncement;
using Project.Services.Contracts;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await serviceManager.AnnouncementService.GetAllAnnouncements(false);
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var announcement = await serviceManager.AnnouncementService.GetAnnouncementById(id, false);
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnouncementInsertDto dto)
        {
            if (dto is null)
                return BadRequest("Announcement data is null");

            var created = await serviceManager.AnnouncementService.CreateAnnouncement(dto);
            return CreatedAtAction(nameof(GetAnnouncementById), new { id = created.AnouncementId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] AnouncementUpdateDto dto)
        {
            if (dto is null)
                return BadRequest("Announcement data is null");

            await serviceManager.AnnouncementService.UpdateAnnouncement(id, dto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await serviceManager.AnnouncementService.DeleteAnnouncement(id, false);
            return NoContent();
        }
    }
}
