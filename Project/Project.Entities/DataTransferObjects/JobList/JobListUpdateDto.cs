using System;

namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListUpdateDto
    {
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public string Title { get; set; } // <-- Yeni alan
        public string Description { get; set; }
    }
}
