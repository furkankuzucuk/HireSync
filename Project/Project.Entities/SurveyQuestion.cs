
namespace Project.Entities;

public class SurveyQuestion
{
    public int SurveyQuestionId { get; set; }
    public int SatisfactionSurveyId { get; set; }  // Anket ID (FK)
    public string QuestionText { get; set; }       // Soru metni
    public string QuestionType { get; set; }       // Soru tipi (çoktan seçmeli, yazılı vs.)
    public ICollection<SurveyAnswer> SurveyAnswers { get; set; }  //
}