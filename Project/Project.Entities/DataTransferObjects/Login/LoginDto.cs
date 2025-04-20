
namespace Project.Entities.DataTransferObjects.Login;

public record LoginDto
{
    public string UserName {get; set;}
    public string Password {get; set;}
}