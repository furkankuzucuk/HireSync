using System;
using Project.Entities.DataTransferObjects.JobList;

namespace Project.Entities.DataTransferObjects.JobApplication
{
    public record JobApplicationDto
    {
        public int JobApplicationId { get; set; }
        public int JobListId { get; set; }
        public int UserId { get; set; }

        public DateTime AppDate { get; set; }
        public string ResumePath { get; set; }
        public string Status { get; set; }
        public string UserFullName { get; set; }


        // Ek görsel alanlar (UI için)
        public string Title { get; set; }              // İlan başlığı
        public string DepartmentName { get; set; }     // Departman adı
        public string JobName { get; set; }            // Pozisyon adı

        // Navigation için DTO içeren alan
        public JobListDto JobList { get; set; }
    }
}
