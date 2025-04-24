namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListUpdateDto
    {
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
