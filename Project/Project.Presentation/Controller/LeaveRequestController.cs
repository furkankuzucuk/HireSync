using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.LeaveRequest;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/leaverequests")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public LeaveRequestController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var leaveRequests = await serviceManager.LeaveRequestService.GetAllLeaveRequests(false);
            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveRequestById(int id)
        {
            var leaveRequest = await serviceManager.LeaveRequestService.GetLeaveRequestById(id, false);
            return Ok(leaveRequest);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestInsertDto leaveRequestDto)
        {
            if (leaveRequestDto == null)
                return BadRequest("Leave request data is null");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null){
                return Unauthorized("User ID not found in token.");
            }
            int userId = int.Parse(userIdClaim.Value);

            var createdLeaveRequest = await serviceManager.LeaveRequestService.CreateLeaveRequest(userId,leaveRequestDto);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { id = createdLeaveRequest.LeaveRequestId }, createdLeaveRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] LeaveRequestUpdateDto leaveRequestDto)
        {
            if (leaveRequestDto == null)
                return BadRequest("Leave request data is null");

            await serviceManager.LeaveRequestService.UpdateLeaveRequest(id, leaveRequestDto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            await serviceManager.LeaveRequestService.DeleteLeaveRequest(id, false);
            return NoContent();
        }
    }
}
