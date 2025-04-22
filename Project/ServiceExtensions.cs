using Microsoft.EntityFrameworkCore;
using Project.Repository;
using Project.Repository.Concretes;
using Project.Repository.Contracts;
using Project.Services.Concretes;
using Project.Services.Contracts;
using Project.Services.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                };
            });

        // Authorization Policy ekle (rol bazlı yetkilendirme)
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Worker", policy => policy.RequireRole("Worker"));
        });
    }

    // Bu metot login/authenticate endpoint'inin API'ye eklenmesi için kullanılabilir
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);  // Authentication ve Authorization ekler
    }

}
