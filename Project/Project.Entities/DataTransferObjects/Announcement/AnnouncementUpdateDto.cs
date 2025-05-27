namespace Project.Entities.DataTransferObjects.Announcement;

public record AnnouncementUpdateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
   
}