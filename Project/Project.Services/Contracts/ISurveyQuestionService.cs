
using Project.Entities.DataTransferObjects;

namespace Project.Services.Contracts;

public interface ISurveyQuestionService
{
    public Task<SurveyQuestionDto> CreateSurveyQuestion(SurveyQuestionInsertDto questionDto);
    public Task<IEnumerable<SurveyQuestionDto>> GetAllSurveyQuestions(bool trackChanges);
}