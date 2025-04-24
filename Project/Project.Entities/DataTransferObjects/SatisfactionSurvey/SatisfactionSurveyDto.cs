namespace Project.Entities.DataTransferObjects.SatisfactionSurvey
{
    public record SatisfactionSurveyDto
    {
        public int SatisfactionSurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } // Optional, to include Department name
    }
}
