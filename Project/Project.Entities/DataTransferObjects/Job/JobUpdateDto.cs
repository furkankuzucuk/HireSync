
namespace Project.Entities.DataTransferObjects.Job;

public record JobUpdateDto
{
    public int DepartmentId { get; set; }

    public string JobName { get; set; }
}