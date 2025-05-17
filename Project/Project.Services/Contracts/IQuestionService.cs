
using Project.Entities;
using Project.Entities.DataTransferObjects;

namespace Project.Services.Contracts;

public interface IQuestionService 
{
    Task<IEnumerable<QuestionDto>> GetAllQuestions(bool trackChanges);
    Task<QuestionDto> GetQuestionById(int id,bool trackChanges);
    Task<IEnumerable<QuestionDto>> GetQuestionsByExamId(int examId, bool trackChanges);    
    Task<QuestionDto> CreateQuestion(QuestionInsertDto questionDto);
    Task DeleteQuestion(int id,bool trackChanges);
    Task UpdateQuestion(int id,QuestionUpdateDto questionDto,bool trackChanges);



}