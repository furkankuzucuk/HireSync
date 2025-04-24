namespace Project.Entities.DataTransferObjects.SatisfactionSurvey
{
    public record SatisfactionSurveyUpdateDto
    {
        public string SurveyTitle { get; set; }
        public string SurveyType { get; set; }
        public int DepartmentId { get; set; }
    }
}
