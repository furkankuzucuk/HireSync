namespace Project.Entities.DataTransferObjects.SatisfactionSurvey
{
    public record SatisfactionSurveyInsertDto
    {
        public string SurveyTitle { get; set; }
        public string SurveyType { get; set; }
    }
}
