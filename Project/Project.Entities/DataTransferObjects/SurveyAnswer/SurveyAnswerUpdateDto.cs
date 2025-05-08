
namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveyAnswerUpdateDto
{
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public int UserId { get; set; }             // Kullanıcı ID (FK)
    public string Answer { get; set; }     
}