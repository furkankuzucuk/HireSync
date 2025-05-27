namespace Project.Entities.DataTransferObjects.Announcement;

public record AnnouncementDto
{
    public int AnnouncementId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    
}