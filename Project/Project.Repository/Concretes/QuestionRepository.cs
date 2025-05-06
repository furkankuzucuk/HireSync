
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes;

public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
{
    public QuestionRepository(RepositoryContext context) : base(context)
    {
    }

    public void CreateQuestion(Question question)
    {
        Create(question);
    }

    public void DeleteQuestion(Question question)
    {
        Delete(question);
    }

    public IQueryable<Question> GetAllQuestions(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public IQueryable<Question> GetQuestionByExamId(int id, bool trackChanges)
    {
        return FindByCondition(q => q.ExamId == id,trackChanges);
    }

    public IQueryable<Question> GetQuestionById(int id, bool trackChanges)
    {
        return FindByCondition(q => q.QuestionId == id,trackChanges);
    }

    public void UpdateQuestion(Question question)
    {
        Update(question);
    }
}