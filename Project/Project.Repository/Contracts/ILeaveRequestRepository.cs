using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        IQueryable<LeaveRequest> GetAllLeaveRequests(bool trackChanges);
        IQueryable<LeaveRequest> GetLeaveRequestById(int id, bool trackChanges);
        IQueryable<LeaveRequest> GetLeaveRequestsByUserId(int userId); // ✅ yeni
        void CreateLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void DeleteLeaveRequest(LeaveRequest leaveRequest);
    }
}
