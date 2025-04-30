namespace Project.Entities;

public class Candidate
{
    public int CandidateId {get; set;}
    public string Name {get; set;}
    public string SurName {get; set;}
    public string Phone {get; set;}
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; }
    public string UserName {get; set;}
    public string Mail{get; set;}
    public string Password {get; set;}
    public string ResumePath {get; set;}
}