namespace Project.Entities.DataTransferObjects.Announcement;

public record AnnouncementInsertDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    
}