using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.JobList;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/joblists")]
    public class JobListController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public JobListController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobLists()
        {
            var jobLists = await serviceManager.JobListService.GetAllJobLists(false);
            return Ok(jobLists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobListById(int id)
        {
            var jobList = await serviceManager.JobListService.GetJobListById(id, false);
            return Ok(jobList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobList([FromBody] JobListInsertDto jobListDto)
        {
            if (jobListDto == null)
                return BadRequest("Job List data is null");

            var createdJobList = await serviceManager.JobListService.CreateJobList(jobListDto);
            return CreatedAtAction(nameof(GetJobListById), new { id = createdJobList.JobListId }, createdJobList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobList(int id, [FromBody] JobListUpdateDto jobListDto)
        {
            if (jobListDto == null)
                return BadRequest("Job List data is null");

            await serviceManager.JobListService.UpdateJobList(id, jobListDto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobList(int id)
        {
            await serviceManager.JobListService.DeleteJobList(id, false);
            return NoContent();
        }
    }
}
