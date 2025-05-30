
using Project.Entities.DataTransferObjects;

namespace Project.Services.Contracts;

public interface ISurveyQuestionService
{
    public Task<SurveyQuestionDto> CreateSurveyQuestion(SurveyQuestionInsertDto questionDto);
    public Task<IEnumerable<SurveyQuestionDto>> GetAllSurveyQuestions(bool trackChanges);
    Task<IEnumerable<SurveyQuestionDto>> GetSurveyQuestionsBySurveyId(int surveyId, bool trackChanges);
    public Task DeleteSurveyQuestion(int id, bool trackChanges);
    public Task<IEnumerable<SurveyQuestionResultDto>> GetSurveyResultsGroupedByQuestion(int surveyId);
    public Task<SurveyQuestionDto> GetSurveyQuestionById(int id, bool trackChanges);
    public Task UpdateSurveyQuestion(int id,SurveyQuestionUpdateDto surveyQuestion,bool trackChanges);
}