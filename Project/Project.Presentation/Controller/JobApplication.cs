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

        [HttpPost]
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationInsertDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
                return BadRequest("Job Application data is null");

            var createdJobApplication = await serviceManager.JobApplicationService.CreateJobApplication(jobApplicationDto);
             return CreatedAtAction(nameof(GetApplicationsByCandidateId), new { candidateId = createdJobApplication.CandidateId }, createdJobApplication);
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

            await serviceManager.JobApplicationService.UpdateJobApplication(id, jobApplicationDto, false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            await serviceManager.JobApplicationService.DeleteJobApplication(id, false);
            return NoContent();
        }
    }
}
