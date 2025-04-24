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

        public async Task<LeaveRequestDto> CreateLeaveRequest(LeaveRequestInsertDto leaveRequest)
        {
            var entity = mapper.Map<LeaveRequest>(leaveRequest);
            repositoryManager.LeaveRequestRepository.CreateLeaveRequest(entity);
            await repositoryManager.Save();
            return mapper.Map<LeaveRequestDto>(entity);
        }

        public async Task DeleteLeaveRequest(int id, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository.GetLeaveRequestById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            repositoryManager.LeaveRequestRepository.DeleteLeaveRequest(entity);
            await repositoryManager.Save();
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequests(bool trackChanges)
        {
            var leaveRequests = await repositoryManager.LeaveRequestRepository.GetAllLeaveRequests(trackChanges).ToListAsync();
            return mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
        }

        public async Task<LeaveRequestDto> GetLeaveRequestById(int id, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository.GetLeaveRequestById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            return mapper.Map<LeaveRequestDto>(entity);
        }

        public async Task UpdateLeaveRequest(int id, LeaveRequestUpdateDto leaveRequest, bool trackChanges)
        {
            var entity = await repositoryManager.LeaveRequestRepository.GetLeaveRequestById(id, trackChanges).FirstOrDefaultAsync();
            if (entity == null)
                throw new EntityNotFoundException<LeaveRequest>(id);

            mapper.Map(leaveRequest, entity);
            await repositoryManager.Save();
        }
    }
}
