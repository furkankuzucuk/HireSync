namespace Project.Entities;

public class SatisfactionSurvey{
    public int SatisfactionSurveyId { get; set; }   // Primary Key
    public string SurveyTitle { get; set; }  // Nvarchar(25)
    public string SurveyType { get; set; }   // Varchar(50)

    public int DepartmentId { get; set; }  // Varbinary(64) (byte array olarak tanımlanır)
    public Department Department {get; set;}
}