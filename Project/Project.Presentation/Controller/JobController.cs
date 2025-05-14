using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Job;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public JobController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await serviceManager.JobService.GetAllJobs(trackChanges: false);
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById([FromRoute] int id)
        {
            var job = await serviceManager.JobService.GetJobById(id, trackChanges: false);
            return Ok(job);
        }

        [HttpGet("department/{departmentId}")]
public async Task<IActionResult> GetJobsByDepartmentId(int departmentId)
{
    var jobs = await serviceManager.JobService.GetJobsByDepartmentId(departmentId, false);
    return Ok(jobs);
}

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobInsertDto jobDto)
        {
            if (jobDto is null)
                return BadRequest("Job data is null");

            var createdJob = await serviceManager.JobService.CreateJob(jobDto);
            return CreatedAtAction(nameof(GetJobById), new { id = createdJob.JobId }, createdJob);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob([FromRoute] int id, [FromBody] JobUpdateDto jobDto)
        {
            if (jobDto is null)
                return BadRequest("Job data is null");

            await serviceManager.JobService.UpdateJob(id, jobDto, trackChanges: true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] int id)
        {
            await serviceManager.JobService.DeleteJob(id, trackChanges: false);
            return NoContent();
        }
    }
}
