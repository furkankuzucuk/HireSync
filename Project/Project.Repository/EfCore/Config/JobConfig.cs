using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Repository.EfCore.Config;

public class JobConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        
        builder.HasData(
            new Job
            {
                JobId = 1,
                JobName = "Software Developer",
                DepartmentId = 1 // Buraya uygun bir Department da olmalı
            },
            new Job
            {
                JobId = 2,
                JobName = "HR Specialist",
                DepartmentId = 1
               }
            );
    }
}