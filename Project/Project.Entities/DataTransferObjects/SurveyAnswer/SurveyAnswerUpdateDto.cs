
namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveyAnswerUpdateDto
{
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public string Answer { get; set; }     
}