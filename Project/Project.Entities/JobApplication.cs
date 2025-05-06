namespace Project.Entities;
public class JobApplication
{
    public int JobApplicationId { get; set; }  // Primary Key
    public int UserId {get; set;}
    public int JobListId { get; set; }  // Foreign Key
    public JobList JobList { get; set; }  // Navigation property to Job
    public DateTime AppDate { get; set; }  // Date
    public string ResumePath {get; set;}
    public string Status { get; set; }     // Nvarchar(20)
}