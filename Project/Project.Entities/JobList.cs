namespace Project.Entities;

public class JobList{
    public int JobListId { get; set; }  // Primary Key

    public int DepartmentId { get; set; }  // Foreign Key
    public string DepartmentName {get; set;}
    public Department Department { get; set; }   // Navigation property to User

    public string Description { get; set; }
    public DateTime CreateDate { get; set; }

}