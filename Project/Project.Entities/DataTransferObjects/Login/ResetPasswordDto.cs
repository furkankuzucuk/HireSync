
namespace Project.Entities.DataTransferObjects.Login;

public class ResetPasswordDto
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}