namespace Project.Entities;

 public class Login
 {
     public int LoginId { get; set; } //kullanıcı işe alındıktan sonra ilk çalışan ekleme ardından login ekleme sayfasına 
     public int UserId { get; set; }
     public User User { get; set; }

     public string UserName { get; set; }
     public string Password { get; set; }
     public string Mail {get; set;}
     public DateTime CreatedAt { get; set; }
     public DateTime? LastLogin { get; set; }
 }