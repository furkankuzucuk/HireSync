using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface ISatisfactionSurveyRepository : IRepositoryBase<SatisfactionSurvey>
    {
        IQueryable<SatisfactionSurvey> GetAllSatisfactionSurveys(bool trackChanges);
        IQueryable<SatisfactionSurvey> GetSatisfactionSurveyById(int id, bool trackChanges);
        void CreateSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey);
        void UpdateSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey);
        void DeleteSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey);
    }
}
