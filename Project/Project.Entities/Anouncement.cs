namespace Project.Entities;

public class Anouncement
{
    public int AnouncementId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}