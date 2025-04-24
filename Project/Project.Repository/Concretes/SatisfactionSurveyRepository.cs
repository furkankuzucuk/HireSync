using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class SatisfactionSurveyRepository : RepositoryBase<SatisfactionSurvey>, ISatisfactionSurveyRepository
    {
        public SatisfactionSurveyRepository(RepositoryContext context) : base(context) { }

        public IQueryable<SatisfactionSurvey> GetAllSatisfactionSurveys(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<SatisfactionSurvey> GetSatisfactionSurveyById(int id, bool trackChanges) =>
            FindByCondition(ss => ss.SatisfactionSurveyId == id, trackChanges);

        public void CreateSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey) =>
            Create(satisfactionSurvey);

        public void UpdateSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey) =>
            Update(satisfactionSurvey);

        public void DeleteSatisfactionSurvey(SatisfactionSurvey satisfactionSurvey) =>
            Delete(satisfactionSurvey);
    }
}
