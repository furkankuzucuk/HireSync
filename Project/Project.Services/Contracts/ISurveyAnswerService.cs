
using Project.Entities.DataTransferObjects.SurveyAnswer;

namespace Project.Services.Contracts;

public interface ISurveyAnswerService
{
     public Task<SurveyAnswerDto> CreateSurveyAnswer(SurveyAnswerInsertDto answerDto);
     public Task<IEnumerable<SurveyAnswerDto>> GetAllSurveyAnswers(bool trackChanges);
      public Task<SurveyAnswerDto> GetSurveyAnswerById(int id, bool trackChanges);
      public Task<IEnumerable<SurveyAnswerDto>> GetSurveyAnswersByQuestionId(int questionId, bool trackChanges);
      public Task DeleteSurveyAnswer(int id, bool trackChanges);
      public Task UpdateSurveyAnswer(int id,SurveyAnswerUpdateDto surveyDto,bool trackChanges);
}