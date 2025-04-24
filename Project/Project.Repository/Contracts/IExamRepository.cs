using Project.Entities;

namespace Project.Repository.Contracts
{
    public interface IExamRepository : IRepositoryBase<Exam>
    {
        IQueryable<Exam> GetAllExams(bool trackChanges);
        IQueryable<Exam> GetExamById(int id, bool trackChanges);
        void CreateExam(Exam exam);
        void UpdateExam(Exam exam);
        void DeleteExam(Exam exam);
    }
}
