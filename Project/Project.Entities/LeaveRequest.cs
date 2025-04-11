namespace Project.Entities;

public class LeaveRequest{
     public int LeaveRequestId { get; set; }  // Primary Key

     public int UserId { get; set; }  // Foreign Key
     public User User { get; set; }   // Navigation property to User

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string LeaveType { get; set; }  // Nvarchar(10)
    public string Status { get; set; }     // Nvarchar(10)

    public DateTime RequestDate { get; set; }  // DateTime
}