using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project.Services;
using Project.Services.Concretes;
using Project.Services.Contracts;
using Project.Services.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve diğer servisler
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

// Email servisini kaydet
builder.Services.AddSingleton(smtpSettings);


// ✅ CORS EKLENDİ
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Project.Presentation.AssemblyReference).Assembly);

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ CORS middleware'i AUTHENTICATION'dan ÖNCE çağrılmalı
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
