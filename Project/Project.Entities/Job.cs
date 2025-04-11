namespace Project.Entities;

public class Job{
    public int JobId { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }

    public string JobName { get; set; }

    public ICollection<User> Users { get; set; }
}
 