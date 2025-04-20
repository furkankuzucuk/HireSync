
namespace Project.Entities.DataTransferObjects.Login;

public record LoginDtoInsertion
{
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public int UserId { get; set; }
}