using Project.Entities;

namespace Project.Repository.Contracts;

public interface IUserAnswerRepository : IRepositoryBase<UserAnswer>
{
        Task<UserAnswer> GetUserAnswerByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<UserAnswer>> GetUserAnswersForExamAsync(int userId, int examId, bool trackChanges);
        void CreateUserAnswer(UserAnswer userAnswer);
        void CreateUserAnswers(IEnumerable<UserAnswer> userAnswers);
        void DeleteUserAnswer(UserAnswer userAnswer);
    
}