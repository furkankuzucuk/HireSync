


namespace Project.Entities.DataTransferObjects.Login;

public record LoginResponseDto
{
    public string Token {get; set;}
    public string Role {get; set;}
}