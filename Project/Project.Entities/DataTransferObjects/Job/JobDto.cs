
namespace Project.Entities.DataTransferObjects.Job;

public record JobDto
{
    public int JobId { get; set; }
    public int DepartmentId { get; set; }

    public string JobName { get; set; }
}

