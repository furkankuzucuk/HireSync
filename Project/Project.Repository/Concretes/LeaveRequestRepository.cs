using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class LeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(RepositoryContext context) : base(context) { }

        public IQueryable<LeaveRequest> GetAllLeaveRequests(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<LeaveRequest> GetLeaveRequestById(int id, bool trackChanges) =>
            FindByCondition(lr => lr.LeaveRequestId == id, trackChanges);

        public void CreateLeaveRequest(LeaveRequest leaveRequest) =>
            Create(leaveRequest);

        public void UpdateLeaveRequest(LeaveRequest leaveRequest) =>
            Update(leaveRequest);

        public void DeleteLeaveRequest(LeaveRequest leaveRequest) =>
            Delete(leaveRequest);
    }
}
