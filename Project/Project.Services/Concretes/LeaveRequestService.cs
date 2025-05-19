using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects.LeaveRequest;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IMapper mapper;

        public LeaveRequestService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this.repositoryManager = repositoryManager;
            this.mapper = mapper;
        }

        public async Task<LeaveRequestDto> CreateLeaveRequest(int userId, LeaveRequestInsertDto leaveRequest)
        {
            var entity = mapper.Map<LeaveRequest>(leaveRequest);
            entity.UserId = userId;
            entity.Status = "Pending";
            entity.RequestDate = DateTime.UtcNow;

            repositoryManager.LeaveRequestRepository.CreateLeaveRequest(entity);
            await repositoryManager.Save();
            return mapper.Map<LeaveRequestDto>(entity);
        }

        public async Task DeleteLeaveRequest(int id, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository
                .GetLeaveRequestById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            repositoryManager.LeaveRequestRepository.DeleteLeaveRequest(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequests(bool trackChanges)
{
    var leaveRequests = await repositoryManager.LeaveRequestRepository
        .GetAllLeaveRequests(trackChanges)
        .Include(lr => lr.User) // ⬅ User join
        .ToListAsync();

    var result = leaveRequests.Select(lr => new LeaveRequestDto
    {
        LeaveRequestId = lr.LeaveRequestId,
        UserId = lr.UserId,
        UserName = $"{lr.User.Name} {lr.User.LastName}", // ⬅ Ad Soyad
        StartDate = lr.StartDate,
        EndDate = lr.EndDate,
        LeaveType = lr.LeaveType,
        Status = lr.Status,
        RequestDate = lr.RequestDate
    });

    return result;
}



        public async Task<LeaveRequestDto> GetLeaveRequestById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository
                .GetLeaveRequestById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            return mapper.Map<LeaveRequestDto>(entity);
        }

        public async Task UpdateLeaveRequest(int id, LeaveRequestUpdateDto leaveRequest, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository
                .GetLeaveRequestById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            mapper.Map(leaveRequest, entity);
            await repositoryManager.Save();
        }

        // ✅ Çalışanın kendi izin başvurularını getirme
        public async Task<IEnumerable<LeaveRequestDto>> GetRequestsByUserId(int userId)
        {
            var requests = await repositoryManager.LeaveRequestRepository
                .GetLeaveRequestsByUserId(userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<LeaveRequestDto>>(requests);
        }
    }
}
