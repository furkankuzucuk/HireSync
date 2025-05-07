
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Services.Contracts;

public interface ISurveyQuestionRepository : IRepositoryBase<SurveyQuestion>
{
    public IQueryable<SurveyQuestion> GetAllSurveyQuestion(bool trackChanges);
    public IQueryable<SurveyQuestion> GetSurveyQuestionById(int id, bool trackChanges);
    public void CreateSurveyQuestion(SurveyQuestion question);
    public void UpdateSurveyQuestion(SurveyQuestion question);
    public void DeleteSurveyQuestion(SurveyQuestion question);
}