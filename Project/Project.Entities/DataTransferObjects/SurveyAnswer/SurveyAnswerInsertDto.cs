
namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveyAnswerInsertDto
{
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public int UserId { get; set; }             // Kullanıcı ID (FK)
    public string Answer { get; set; }     
}