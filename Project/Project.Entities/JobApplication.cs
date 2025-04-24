namespace Project.Entities;
public class JobApplication
{
    public int JobApplicationId { get; set; }  // Primary Key

    public int JobId { get; set; }  // Foreign Key
    public Job Job { get; set; }  // Navigation property to Job
    public string AppMail { get; set; }  // Nvarchar(50)
    public string Location { get; set; } // Nvarchar(50)

    public DateTime AppDate { get; set; }  // Date

    public string ResumePath { get; set; } // Nvarchar(255)
    public string Status { get; set; }     // Nvarchar(20)
}