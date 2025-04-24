using Project.Entities;
using Project.Entities.DataTransferObjects.Exam;

namespace Project.Services.Contracts
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetAllExams(bool trackChanges);
        Task<ExamDto> GetExamById(int id, bool trackChanges);
        Task<ExamDto> CreateExam(ExamInsertDto exam);
        Task UpdateExam(int id, ExamUpdateDto exam, bool trackChanges);
        Task DeleteExam(int id, bool trackChanges);
    }
}
