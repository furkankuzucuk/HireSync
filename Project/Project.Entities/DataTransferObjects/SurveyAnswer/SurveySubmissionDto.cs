
namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveySubmissionDto
{
    public int SatisfactionSurveyId { get; set; }
    public List<SurveyAnswerInsertDto> Answers { get; set; }
}