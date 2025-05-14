
namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveyAnswerInsertDto
{
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public string Answer { get; set; }     
}