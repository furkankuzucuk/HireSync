namespace Project.Entities.DataTransferObjects.LeaveRequest
{
    public record LeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } // Ad Soyad
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
