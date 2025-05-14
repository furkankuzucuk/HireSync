namespace Project.Entities;

public class Department{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    

    public ICollection<Job> Jobs { get; set; }
}