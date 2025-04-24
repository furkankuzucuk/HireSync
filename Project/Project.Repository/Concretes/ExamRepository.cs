using Project.Entities;
using Project.Repository.Contracts;

namespace Project.Repository.Concretes
{
    public class ExamRepository : RepositoryBase<Exam>, IExamRepository
    {
        public ExamRepository(RepositoryContext context) : base(context) { }

        public IQueryable<Exam> GetAllExams(bool trackChanges) =>
            FindAll(trackChanges);

        public IQueryable<Exam> GetExamById(int id, bool trackChanges) =>
            FindByCondition(exam => exam.ExamId == id, trackChanges);

        public void CreateExam(Exam exam) =>
            Create(exam);

        public void UpdateExam(Exam exam) =>
            Update(exam);

        public void DeleteExam(Exam exam) =>
            Delete(exam);
    }
}
