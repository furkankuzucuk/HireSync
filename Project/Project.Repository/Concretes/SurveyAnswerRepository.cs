
using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class SurveyAnswerRepository : RepositoryBase<SurveyAnswer>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(RepositoryContext context) : base(context) { }

        public void CreateSurveyAnswer(SurveyAnswer answer)
        {
            Create(answer);
        }

        public IQueryable<SurveyAnswer> GetAllSurveyAnswers(bool trackChanges)
        {
            return FindAll(trackChanges);
        }

        public IQueryable<SurveyAnswer> GetSurveyAnswersByQuestionId(int questionId, bool trackChanges)
        {
            return FindByCondition(a => a.SurveyQuestionId == questionId, trackChanges);
        }

        public void DeleteSurveyAnswer(SurveyAnswer answer)
        {
            Delete(answer);
        }

        public void UpdateSurveyAnswer(SurveyAnswer answer)
        {
            Update(answer);
        }
    }
}
