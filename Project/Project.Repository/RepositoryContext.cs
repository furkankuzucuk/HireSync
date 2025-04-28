using System.Reflection;
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
    public DbSet<JobApplication> JobApplications { get; set; }  // JobApplication DbSet eklendi
    public DbSet<SatisfactionSurvey> SatisfactionSurveys {get; set;}
    public DbSet<JobList> JobLists {get; set;}
    public DbSet<Candidate> Candidates {get; set;}
    public DbSet<UserExam> UserExams {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .Property(u => u.Salary)
        .HasPrecision(18, 2); // 18 toplam basamak, 2 ondalık

        base.OnModelCreating(modelBuilder);
       // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
   
    }
}
