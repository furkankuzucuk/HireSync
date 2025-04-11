namespace Project.Entities;

public class User{
     public int UserId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public decimal Salary  { get; set; }

    public int JobId { get; set; } 
    public Job Job { get; set; } //JobId nin Jobs sınıfından geldiğini belirlemek için 

    public ICollection<Login> Logins { get; set; } //logine bağlantı kurulacak 1 e çok ilişki 
}