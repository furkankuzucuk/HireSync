using Project.Entities;
using Project.Entities.DataTransferObjects.SatisfactionSurvey;

namespace Project.Services.Contracts
{
    public interface ISatisfactionSurveyService
    {
        Task<IEnumerable<SatisfactionSurveyDto>> GetAllSatisfactionSurveys(bool trackChanges);
        Task<SatisfactionSurveyDto> GetSatisfactionSurveyById(int id, bool trackChanges);
        Task<IEnumerable<SatisfactionSurveyDto>> GetSurveysByUserDepartment(int userId);
        Task<SatisfactionSurveyDto> CreateSatisfactionSurvey(SatisfactionSurveyInsertDto satisfactionSurvey);
        Task UpdateSatisfactionSurvey(int id, SatisfactionSurveyUpdateDto satisfactionSurvey, bool trackChanges);
        Task DeleteSatisfactionSurvey(int id, bool trackChanges);
    }
}
