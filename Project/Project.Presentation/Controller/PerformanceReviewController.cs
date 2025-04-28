using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.PerformanceReview;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/performancereviews")]
    public class PerformanceReviewController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public PerformanceReviewController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPerformanceReviews()
        {
            var reviews = await serviceManager.PerformanceReviewService.GetAllPerformanceReviews(false);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformanceReviewById(int id)
        {
            var review = await serviceManager.PerformanceReviewService.GetPerformanceReviewById(id, false);
            return Ok(review);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var reviews = await serviceManager.PerformanceReviewService.GetReviewsByUserId(userId, false);
            return Ok(reviews);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePerformanceReview([FromBody] PerformanceReviewInsertDto performanceReviewDto)
        {
            if (performanceReviewDto == null)
                return BadRequest("Performance review data is null");

            var createdReview = await serviceManager.PerformanceReviewService.CreatePerformanceReview(performanceReviewDto);
            return CreatedAtAction(nameof(GetPerformanceReviewById), new { id = createdReview.PerformanceReviewId }, createdReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceReview(int id, [FromBody] PerformanceReviewUpdateDto performanceReviewDto)
        {
            if (performanceReviewDto == null)
                return BadRequest("Performance review data is null");

            await serviceManager.PerformanceReviewService.UpdatePerformanceReview(id, performanceReviewDto, false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReview(int id)
        {
            await serviceManager.PerformanceReviewService.DeletePerformanceReview(id, false);
            return NoContent();
        }
    }
}
