using System;

namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListDto
    {
        public int JobListId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string Title { get; set; } // <-- Yeni alan
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
