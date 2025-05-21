namespace Project.Entities.DataTransferObjects.Anouncement;

public record AnouncementInsertDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}