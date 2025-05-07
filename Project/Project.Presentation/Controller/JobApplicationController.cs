using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Services.Contracts;

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

        [HttpPost] //başvuru oluşturma cv ile beraber 
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationInsertDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
            {
                return BadRequest("Job Application data or file is null.");
            }
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null){
                return Unauthorized("User ID not found in token.");
            }
            int userId = int.Parse(userIdClaim.Value);
 
            var createdJobApplication = await serviceManager.JobApplicationService.CreateJobApplication(userId,jobApplicationDto);

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
        public async Task<IActionResult> UploadResume([FromForm] IFormFile file, [FromForm] int jobApplicationId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
            
            // Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // JobApplication verisini al
            var jobApplication = await serviceManager.JobApplicationService.GetJobApplicationById(jobApplicationId, false);
            if (jobApplication == null)
            {
                return NotFound($"Job application with ID {jobApplicationId} not found.");
            }

            // JobApplication'ı güncelle
            var jobApplicationUpdateDto = new JobApplicationUpdateDto
            {
                ResumePath = "/uploads/" + file.FileName, // Web'e açık URL'i veritabanına kaydediyoruz
            };

            // Güncelleme işlemini yap
            await serviceManager.JobApplicationService.UpdateJobApplication(jobApplicationId, jobApplicationUpdateDto, true);

            // Dosya yolunu döndür
            return Ok(new { FilePath = "/uploads/" + file.FileName });
        }

        [HttpGet("download/{id}")] //cv goruntuleme
        public async Task<IActionResult> DownloadResume(int id)
        {
            // Başvuru verisini al
            var jobApplication = await serviceManager.JobApplicationService.GetJobApplicationById(id, false);
            if (jobApplication == null)
            {
                return NotFound($"Job application with ID {id} not found.");
            }

            // CV dosyasının yolunu al
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", jobApplication.ResumePath.TrimStart('/'));

            // Dosyayı var mı kontrol et
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            // Dosyayı döndür
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
