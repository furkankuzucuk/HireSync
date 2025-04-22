
namespace Project.Entities.DataTransferObjects.Job;

public record JobInsertDto
{
    public int DepartmentId { get; set; }

    public string JobName { get; set; }
}