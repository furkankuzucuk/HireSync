using Microsoft.EntityFrameworkCore;
using Project.Repository;

public static class ServiceExtensions 
{
    public static void ConfigureSqlContext(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}