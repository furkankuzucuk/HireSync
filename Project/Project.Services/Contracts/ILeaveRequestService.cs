using Project.Entities;
using Project.Entities.DataTransferObjects.LeaveRequest;

namespace Project.Services.Contracts
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequests(bool trackChanges);
        Task<LeaveRequestDto> GetLeaveRequestById(int id, bool trackChanges);
        Task<LeaveRequestDto> CreateLeaveRequest(int id,LeaveRequestInsertDto leaveRequest);
        Task UpdateLeaveRequest(int id, LeaveRequestUpdateDto leaveRequest, bool trackChanges);
        Task DeleteLeaveRequest(int id, bool trackChanges);
    }
}
