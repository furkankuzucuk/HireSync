namespace Project.Entities.DataTransferObjects.JobList
{
    public class JobListDto
    {
        public int JobListId { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
