
namespace Project.Entities.DataTransferObjects;

public record SurveyQuestionResultDto
{
    public string QuestionText { get; set; }
    public Dictionary<string, int> AnswerDistribution { get; set; }
}
