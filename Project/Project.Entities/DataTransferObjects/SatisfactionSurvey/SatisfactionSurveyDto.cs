namespace Project.Entities.DataTransferObjects.SatisfactionSurvey
{
    public record SatisfactionSurveyDto
    {
        public int SatisfactionSurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyType { get; set; }
    }
}
