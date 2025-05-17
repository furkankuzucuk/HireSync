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

        [HttpPost("generate-review/{userId}")]
        public async Task<IActionResult> GeneratePerformanceReview(int userId)
        {
            var review = await serviceManager.PerformanceReviewService.GeneratePerformanceReviewForUser(userId);
            return Ok(review);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterReviews([FromQuery] int? userId, [FromQuery] int? examId)
        {
            var all = await serviceManager.PerformanceReviewService.GetAllPerformanceReviews(false);
            var filtered = all;

            if (userId.HasValue)
                filtered = filtered.Where(r => r.UserExamId == userId).ToList(); // bu sadece örnek

            return Ok(filtered);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceReview(int id, [FromBody] PerformanceReviewUpdateDto performanceReviewDto)
        {
            if (performanceReviewDto == null)
                return BadRequest("Performance review data is null");

            await serviceManager.PerformanceReviewService.UpdatePerformanceReview(id, performanceReviewDto, true);
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
