using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Entities;
using Project.Entities.DataTransferObjects;
using Project.Entities.Exceptions;
using Project.Repository.Contracts;
using Project.Services.Contracts;

namespace Project.Services.Concretes
{
    public class UserExamService : IUserExamService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserExamService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<UserExamDto> CreateUser(int userId,UserExamInsertDto userExamDto)
        {
            var userExamEntity = _mapper.Map<UserExam>(userExamDto);
            userExamEntity.UserId = userId;
            _repositoryManager.UserExamRepository.CreateUser(userExamEntity);
            await _repositoryManager.Save();
            return _mapper.Map<UserExamDto>(userExamEntity);
        }

        public async Task DeleteOneUser(int id, bool trackChanges)
        {
            var userExam = await _repositoryManager.UserExamRepository
                .GetUserById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (userExam == null)
                throw new EntityNotFoundException<UserExam>(id);

            _repositoryManager.UserExamRepository.DeleteUser(userExam);
            await _repositoryManager.Save();
        }

        public async Task<IEnumerable<UserExamDto>> GetAllUsers(bool trackChanges)
        {
            var userExams = await _repositoryManager.UserExamRepository
                .GetAllUsers(trackChanges)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserExamDto>>(userExams);
        }

        public async Task<IEnumerable<UserExamDto>> GetFilteredUserExams(int? userId, int? examId, bool trackChanges)
        {
                var all = await _repositoryManager.UserExamRepository.FindAll(trackChanges)
                .Include(ue => ue.Exam)
                .Include(ue => ue.User)
                .ToListAsync();

            var filtered = all.AsQueryable();

            if (userId.HasValue)
                filtered = filtered.Where(x => x.UserId == userId);

            if (examId.HasValue)
                filtered = filtered.Where(x => x.ExamId == examId);

            return _mapper.Map<IEnumerable<UserExamDto>>(filtered.ToList());
        }

        public async Task<UserExamDto> GetUserById(int id, bool trackChanges)
        {
            var userExam = await _repositoryManager.UserExamRepository
                .GetUserById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (userExam == null)
                throw new EntityNotFoundException<UserExam>(id);

            return _mapper.Map<UserExamDto>(userExam);
        }

        public async Task<IEnumerable<UserExamDto>> GetUserExamByExamId(int examId, bool trackChanges)
        {
            var userExams = await _repositoryManager.UserExamRepository.GetUserExamByExamId(examId, trackChanges).ToListAsync();
            return _mapper.Map<IEnumerable<UserExamDto>>(userExams);
        }

        public async Task<IEnumerable<UserExamDto>> GetUserExamsByUserId(int userId, bool trackChanges)
        {
            var userExams = await _repositoryManager.UserExamRepository.GetUserExamsByUserIdAsync(userId, trackChanges);
            return _mapper.Map<IEnumerable<UserExamDto>>(userExams);
        }

        public async Task UpdateUser(int id, UserExamUpdateDto userExamDto, bool trackChanges)
        {
            var userExam = await _repositoryManager.UserExamRepository
                .GetUserById(id, trackChanges)
                .FirstOrDefaultAsync();

            if (userExam == null)
                throw new EntityNotFoundException<UserExam>(id);

            _mapper.Map(userExamDto, userExam);
            _repositoryManager.UserExamRepository.UpdateUser(userExam);
            await _repositoryManager.Save();
        }
    }
}
