﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<PerformanceReview> PerformanceReviews { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<SatisfactionSurvey> SatisfactionSurveys { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<JobList> JobLists { get; set; }
    public DbSet<UserExam> UserExams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Announcement> Announcements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // JobList -> Job ilişkisi (senin orijinal kodun)
    modelBuilder.Entity<JobList>()
        .HasOne(j => j.Job)
        .WithMany(j => j.JobLists)
        .HasForeignKey(j => j.JobId)
        .OnDelete(DeleteBehavior.NoAction);

    
}

}
