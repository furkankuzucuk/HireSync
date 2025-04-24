namespace Project.Entities.DataTransferObjects.LeaveRequest
{
    public record LeaveRequestUpdateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string Status { get; set; }
    }
}
