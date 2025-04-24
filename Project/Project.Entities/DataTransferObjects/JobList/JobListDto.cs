namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListDto
    {
        public int JobListId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
