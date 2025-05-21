using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Services.Contracts;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/jobapplications")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public JobApplicationController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // ✅ Admin tüm başvuruları görebilir
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllJobApplications()
        {
            var jobApplications = await serviceManager.JobApplicationService.GetAllJobApplications(false);
            return Ok(jobApplications);
        }

        // ✅ Belirli bir başvuru
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetJobApplicationById(int id)
        {
            var jobApplication = await serviceManager.JobApplicationService.GetJobApplicationById(id, false);
            return Ok(jobApplication);
        }

        // ✅ Aday kendi başvurularını görebilir
        [HttpGet("candidate/{candidateId}")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GetApplicationsByCandidateId(int candidateId)
        {
            var apps = await serviceManager.JobApplicationService.GetApplicationsByCandidateId(candidateId);
            return Ok(apps);
        }

        // ✅ Yeni başvuru oluştur
        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationInsertDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
                return BadRequest("Job Application data is null.");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var usernameClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "username" || c.Type == ClaimTypes.Name);

            if (userIdClaim == null || usernameClaim == null)
                return Unauthorized("User ID or username not found in token.");

            int userId = int.Parse(userIdClaim.Value);
            string username = usernameClaim.Value;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            string expectedFilePath = Path.Combine(uploadsFolder, $"{username}_cv.pdf");

            if (!System.IO.File.Exists(expectedFilePath))
                return BadRequest("Başvuru yapabilmek için lütfen önce CV’nizi yükleyiniz.");

            jobApplicationDto.ResumePath = $"/uploads/{username}_cv.pdf";

            var created = await serviceManager.JobApplicationService.CreateJobApplication(userId, jobApplicationDto);
            return CreatedAtAction(nameof(GetJobApplicationById), new { id = created.JobApplicationId }, created);
        }

        // ✅ CV yükleme endpointi
        [HttpPost("upload")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> UploadResume([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".pdf")
                return BadRequest("Yalnızca PDF dosyaları kabul edilmektedir.");

            var userNameClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "username" || c.Type == ClaimTypes.Name || c.Type == "preferred_username");

            if (userNameClaim == null)
                return Unauthorized("Username not found in token.");

            var username = userNameClaim.Value;
            var uniqueFileName = $"{username}_cv{extension}";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FilePath = "/uploads/" + uniqueFileName });
        }

        // ✅ PDF önizleme (iframe içinde gösterilir)
        [HttpGet("view-pdf/{filename}")]
        [AllowAnonymous]
        public IActionResult ViewPdf(string filename)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(uploadsFolder, filename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("PDF bulunamadı.");

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/pdf");
        }

        // ✅ Admin başvuru durumu güncelleyebilir
        [HttpPut("{id}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> UpdateJobApplication(int id, [FromBody] JobApplicationUpdateDto jobApplicationDto)
{
    if (jobApplicationDto == null)
        return BadRequest("Job Application data is null");

    await serviceManager.JobApplicationService.UpdateJobApplication(id, jobApplicationDto, true);

    return NoContent();
}


        // ✅ Başvuru silme (Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            await serviceManager.JobApplicationService.DeleteJobApplication(id, false);
            return NoContent();
        }
    }
}
