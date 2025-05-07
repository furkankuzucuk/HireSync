using Project.Entities;

namespace Project.Repository.Concretes
{
    public class SurveyQuestionRepository : RepositoryBase<SurveyQuestion>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(RepositoryContext context) : base(context) { }

        public void CreateSurveyQuestion(SurveyQuestion question)
        {
            Create(question);
        }

        public IQueryable<SurveyQuestion> GetAllSurveyQuestions(bool trackChanges)
        {
            return FindAll(trackChanges);
        }

        public IQueryable<SurveyQuestion> GetSurveyQuestionById(int id, bool trackChanges)
        {
            return FindByCondition(q => q.SurveyQuestionId == id, trackChanges);
        }

        public void DeleteSurveyQuestion(SurveyQuestion question)
        {
            Delete(question);
        }
    }
}
