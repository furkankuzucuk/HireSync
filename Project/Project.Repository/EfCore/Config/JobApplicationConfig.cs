using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Repository.EfCore.Config;

public class JobApplicationConfig : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
       builder.HasOne(j => j.User)
            .WithMany()
            .HasForeignKey(j => j.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(j => j.Job)
            .WithMany()
            .HasForeignKey(j => j.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}