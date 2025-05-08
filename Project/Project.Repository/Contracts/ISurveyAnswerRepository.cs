
using Project.Entities;

namespace Project.Repository.Contracts;

public interface ISurveyAnswerRepository
{
    public void CreateSurveyAnswer(SurveyAnswer answer);
    public IQueryable<SurveyAnswer> GetAllSurveyAnswers(bool trackChanges);
    public IQueryable<SurveyAnswer> GetSurveyAnswerById(int questionId,bool trackChanges);
    public IQueryable<SurveyAnswer> GetSurveyAnswersByQuestionId(int questionId, bool trackChanges);
    public void DeleteSurveyAnswer(SurveyAnswer answer);
    public void UpdateSurveyAnswer(SurveyAnswer answer);
}