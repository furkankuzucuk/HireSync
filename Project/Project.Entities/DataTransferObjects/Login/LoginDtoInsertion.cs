
namespace Project.Entities.DataTransferObjects.Login;

public record LoginDtoInsertion
{
        public string UserName { get; set; }
        public string Password { get; set; }
         public string Mail {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
        //public int UserId { get; set; } 
        //login oluştururken userId bilgisini loginId ye eşitlemek mantıklı olur mu ?
}