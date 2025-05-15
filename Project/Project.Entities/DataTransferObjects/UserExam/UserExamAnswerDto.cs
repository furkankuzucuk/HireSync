namespace Project.Entities.DataTransferObjects;

public record UserExamAnswerDto
{
    public int ExamId { get; init; }
    public Dictionary<int, string> Answers { get; init; } // Key: QuestionId, Value: User's answer
}
