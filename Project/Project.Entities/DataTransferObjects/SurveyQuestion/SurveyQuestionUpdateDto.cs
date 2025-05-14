
namespace Project.Entities.DataTransferObjects;

public record SurveyQuestionUpdateDto
{
    public int SatisfactionSurveyId { get; set; }  // Anket ID (FK)
    public string QuestionText { get; set; }       // Soru metni
    public string QuestionType { get; set; }       // Soru tipi (çoktan seçmeli, yazılı vs.)
    
}