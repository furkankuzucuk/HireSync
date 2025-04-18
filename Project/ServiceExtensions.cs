using Microsoft.EntityFrameworkCore;
using Project.Repository;
using Project.Repository.Concretes;
using Project.Repository.Contracts;
using Project.Services.Concretes;
using Project.Services.Contracts;
using Project.Services.Mapper;
using AutoMapper;


public static class ServiceExtensions 
{
    public static void ConfigureSqlContext(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    //  public static void ConfigureMapper(this IServiceCollection services){
    //      services.AddAutoMapper(typeof(MappingProfile));
    //  }
}