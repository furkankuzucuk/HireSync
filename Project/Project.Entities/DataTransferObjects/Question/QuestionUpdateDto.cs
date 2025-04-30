

namespace Project.Entities.DataTransferObjects;

public record QuestionUpdateDto
{
    public int ExamId {get; init;}
    public string QuestionText {get; init;}
    public List<string> AnswerOptions { get; init; }
    public string CorrectAnswer { get; init; }
}