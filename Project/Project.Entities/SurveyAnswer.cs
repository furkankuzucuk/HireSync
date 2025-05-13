namespace Project.Entities;

public class SurveyAnswer
{
    public int SurveyAnswerId { get; set; }
    public int SurveyQuestionId { get; set; }  // Soru ID (FK)
    public SurveyQuestion SurveyQuestion { get; set; } // Navigation Property
    public string Answer { get; set; }          // Kullanıcının cevabı
}
