using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities;

public class JobList
{
    public int JobListId { get; set; } // Primary Key

    public int DepartmentId { get; set; } // Foreign Key
    public Department Department { get; set; } 
    public int JobId { get; set; }

    [ForeignKey("JobId")]
    public Job Job { get; set; }

    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
}
