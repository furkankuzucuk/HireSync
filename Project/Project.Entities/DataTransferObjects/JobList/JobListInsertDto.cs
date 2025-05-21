using System;

namespace Project.Entities.DataTransferObjects.JobList
{
    public record JobListInsertDto
    {

        public int DepartmentId { get; set; }
<<<<<<< Updated upstream
        public int JobId { get; set; }
        public string Title { get; set; } // <-- Yeni alan
=======
        public int JobId { get;set;}
>>>>>>> Stashed changes
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
