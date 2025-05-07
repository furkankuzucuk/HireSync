namespace Project.Entities.DataTransferObjects.User;

public class UserDtoCandidateInsert
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    
    // RoleName'i dışarıdan almayacağız, burada "Candidate" olarak otomatik ayarlayacağız.
    public string RoleName { get; set; } = "Candidate";  // Default olarak Candidate olacak
}
