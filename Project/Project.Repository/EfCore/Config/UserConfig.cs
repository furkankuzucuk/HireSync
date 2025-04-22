using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Repository.EfCore.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Salary).HasPrecision(18,2);
        builder.HasData(
            new User
            {
                UserId = 1,
                Email = "john.doe@example.com",
                Name = "John",
                LastName = "Doe",
                Birthday = new DateTime(1990, 5, 20),
                Phone ="5553330278",
                Address = "New York",
                Gender = "Male",
                RoleName = "Admin",
                JobId = 1,
                Salary = 60000
            },
            new User
            {
                UserId = 2,
                Email = "jane.smith@example.com",
                Name = "User",
                LastName = "Smith",
                Birthday = new DateTime(1992, 3, 15),
                Phone = "5553330279",
                Address = "Los Angeles",
                Gender = "Female",
                RoleName = "Worker",
                JobId = 2,
                Salary = 55000
            }

            );
    }
}