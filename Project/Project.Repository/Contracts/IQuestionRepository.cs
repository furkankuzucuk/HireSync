
using Project.Entities;

namespace Project.Repository.Contracts;

public interface IQuestionRepository : IRepositoryBase<Question>
{
        IQueryable<Question> GetAllQuestions(bool trackChanges);
        IQueryable<Question> GetQuestionById(int id, bool trackChanges);
        IQueryable<Question> GetQuestionByExamId(int id,bool trackChanges);
        void CreateQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(Question question);
}