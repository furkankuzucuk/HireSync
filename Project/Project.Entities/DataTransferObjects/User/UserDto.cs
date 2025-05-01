
namespace Project.Entities.DataTransferObjects.User;

public record UserDto {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string RoleName {get; set;}
    public int JobId { get; set; } 
    
}