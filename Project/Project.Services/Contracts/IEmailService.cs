
namespace Project.Services.Contracts
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string resetLink);
        // Diğer email gönderme metodları eklenebilir
    }
}