
using System.ComponentModel.DataAnnotations;

namespace Project.Entities.DataTransferObjects.Login;

public record LoginAuthenticationDto
{
    [Required(ErrorMessage ="Username is required")]
    public string UserName { get; init; }

    [Required(ErrorMessage ="Password is required")]
    public string Password { get; init; }
}