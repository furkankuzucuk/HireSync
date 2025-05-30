namespace Project.Entities.DataTransferObjects.LeaveRequest
{
    public record LeaveRequestInsertDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
    }
}

