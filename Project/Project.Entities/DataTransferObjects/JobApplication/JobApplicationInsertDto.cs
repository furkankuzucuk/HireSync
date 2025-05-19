namespace Project.Entities.DataTransferObjects.JobApplication
{
    public record JobApplicationInsertDto
    {
        public int JobListId { get; set; }

        public DateTime AppDate { get; set; }
        public string ResumePath { get; set; }
        public string Status { get; set; }
    }
}


