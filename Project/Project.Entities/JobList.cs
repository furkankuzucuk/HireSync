using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities
{
    public class JobList
    {
        public int JobListId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

<<<<<<< Updated upstream
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string Title { get; set; } // <-- Yeni alan
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
=======
    public int DepartmentId { get; set; } // Foreign Key
    public Department Department { get; set; } 

    public int JobId { get; set; } 
        public Job Job { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
>>>>>>> Stashed changes
}
