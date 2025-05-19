using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.LeaveRequest;
using Project.Services.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var leaveRequests = await serviceManager.LeaveRequestService.GetAllLeaveRequests(false);
            return Ok(leaveRequests);
        }

        [HttpGet("user")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> GetCurrentUserRequests()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim.Value);
            var requests = await serviceManager.LeaveRequestService.GetRequestsByUserId(userId);
            return Ok(requests);
        }

        [HttpPost]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestInsertDto leaveRequestDto)
        {
            if (leaveRequestDto == null)
                return BadRequest("Leave request data is null");

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim.Value);
            var created = await serviceManager.LeaveRequestService.CreateLeaveRequest(userId, leaveRequestDto);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { id = created.LeaveRequestId }, created);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLeaveRequestById(int id)
        {
            var item = await serviceManager.LeaveRequestService.GetLeaveRequestById(id, false);
            return Ok(item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] LeaveRequestUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("Update data is null");

            await serviceManager.LeaveRequestService.UpdateLeaveRequest(id, dto, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            await serviceManager.LeaveRequestService.DeleteLeaveRequest(id, false);
            return NoContent();
        }
    }
}
