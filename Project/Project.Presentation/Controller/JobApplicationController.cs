using AutoMapper;
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

        [HttpGet]
        public async Task<IActionResult> GetAllJobApplications()
        {
            var jobApplications = await serviceManager.JobApplicationService.GetAllJobApplications(false);
            return Ok(jobApplications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobApplicationById(int id)
        {
            var jobApplication = await serviceManager.JobApplicationService.GetJobApplicationById(id, false);
            return Ok(jobApplication);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationInsertDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
            {
                return BadRequest("Job Application data or file is null.");
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var createdJobApplication = await serviceManager.JobApplicationService.CreateJobApplication(userId, jobApplicationDto);

            return CreatedAtAction(nameof(GetJobApplicationById), new { id = createdJobApplication.JobApplicationId }, createdJobApplication);
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetApplicationsByCandidateId(int candidateId)
        {
            var apps = await serviceManager.JobApplicationService.GetApplicationsByCandidateId(candidateId);
            return Ok(apps);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobApplication(int id, [FromBody] JobApplicationUpdateDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
                return BadRequest("Job Application data is null");

            await serviceManager.JobApplicationService.UpdateJobApplication(id, jobApplicationDto, true);
            return NoContent();
        }

        [HttpPost("upload")]
public async Task<IActionResult> UploadResume([FromForm] IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");

    var userNameClaim = User.Claims.FirstOrDefault(c =>
        c.Type == "username" || c.Type == ClaimTypes.Name || c.Type == "preferred_username");

    if (userNameClaim == null)
        return Unauthorized("Username not found in token.");

    var username = userNameClaim.Value;
    var fileExtension = Path.GetExtension(file.FileName);
    var uniqueFileName = $"{username}_cv{fileExtension}";
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


        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadResume(int id)
        {
            var jobApplication = await serviceManager.JobApplicationService.GetJobApplicationById(id, false);
            if (jobApplication == null)
            {
                return NotFound($"Job application with ID {id} not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", jobApplication.ResumePath.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            await serviceManager.JobApplicationService.DeleteJobApplication(id, false);
            return NoContent();
        }
    }
}
