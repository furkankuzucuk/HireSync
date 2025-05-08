namespace Project.Entities.DataTransferObjects.SurveyAnswer;

public record SurveyAnswerDto
{
    public int SurveyAnswerId { get; set; }
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public int UserId { get; set; }             // Kullanıcı ID (FK)
    public string Answer { get; set; }          // Kullanıcının cevabı
}
