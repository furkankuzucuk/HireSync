namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListInsertDto
    {
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
