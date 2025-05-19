using System;

namespace Project.Entities
{
    public class JobApplication
    {
        public int JobApplicationId { get; set; }

        public int UserId { get; set; }
        public int JobListId { get; set; }

        public DateTime AppDate { get; set; }    // Başvuru tarihi

        public string ResumePath { get; set; }   // CV dosya yolu
        public string Status { get; set; }       // Başvuru durumu

        // Navigation property
        public JobList JobList { get; set; }
        public User User { get; set; }
    }
}
