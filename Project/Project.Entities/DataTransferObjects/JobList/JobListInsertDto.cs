using System;

namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListInsertDto
    {
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public string Title { get; set; } // <-- Yeni alan
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
